using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.MediaFiles;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Xceed.Words.NET;


namespace Anesis.ApiService.Services.Services
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CustomMediaFile> _mediaFileRepo;
        private readonly IRepository<Signature> _signatureRepo;

        public MediaFileService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediaFileRepo = _unitOfWork.GetRepository<CustomMediaFile>();
            _signatureRepo = _unitOfWork.GetRepository<Signature>();
        }

        public async Task<IPagedList<MediaFileDto>> SearchAsync(MediaFileFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            return await _mapper.ProjectTo<MediaFileDto>(Search(filter))
                .SortData(filter.Sidx, filter.Sord, nameof(CustomMediaFile.FileName))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<MediaFileDto> GetByIdAsync(int id,
            CancellationToken cancellationToken = default)
        {
            return _mapper.Map<MediaFileDto>(await FindByIdAsync(id, cancellationToken));
        }

        public async Task<SignatureDto> GetSignatureFromFileIdAsync(int fileId,
            CancellationToken cancellationToken = default)
        {
            var signature = await FindSignatureFromFileAsync(fileId, cancellationToken);

            if (signature == null)
            {
                return null;
            }

            return new SignatureDto()
            {
                Id = signature.Id,
                FileId = signature.Id,
                ImageBase64 = signature.ImageBase64
            };
        }

        public async Task<bool> UploadAsync(FileUploadDto model,
            CancellationToken cancellationToken = default)
        {
            await _mediaFileRepo.InsertAsync(model.ToMediaFile(), cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SignToFileAsync(int fileId, string imageBase64,
            CancellationToken cancellationToken = default)
        {
            var mediaFile = await FindByIdAsync(fileId, cancellationToken);
            var imageAsBytes = ConvertBase64ToBytes(imageBase64);

            if (mediaFile.FileExt == MediaFileType.PDF)
            {
                SignToPdfFile(mediaFile.FilePath, imageAsBytes);
            }
            else if (MediaFileType.WordTypes().Contains(mediaFile.FileExt))
            {
                SignToWordFile(mediaFile.FilePath, imageAsBytes);
            }

            else if (MediaFileType.ImageTypes().Contains(mediaFile.FileExt))
            {
                SignToImageFile(mediaFile.FilePath, imageAsBytes);
            }

            await _signatureRepo.InsertAsync(new Signature()
            {
                FileId = fileId,
                ImageBase64 = imageBase64,
            }, cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            // Delete all signatures from file
            await _signatureRepo.DeleteAllAsync(x => x.FileId == id, cancellationToken);

            // Delete file
            await _mediaFileRepo.DeleteAsync(cancellationToken, id);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        // SUPPORT METHODS
        public IQueryable<CustomMediaFile> Search(MediaFileFilterDto filter)
        {
            return _mediaFileRepo.All(true)
                .WhereIf(x => x.FileExt == filter.FileExt, filter.FileExt.HasValue())
                .WhereIf(x => x.Category == filter.Category, filter.Category.HasValue())
                .WhereIf(x => x.Signatures.Count == 0, filter.NotSignedOnly);
        }

        public async Task<CustomMediaFile> FindByIdAsync(int id,
            CancellationToken cancellationToken = default)
        {
            return await _mediaFileRepo.FindAsync(cancellationToken, id);
        }

        public async Task<Signature> FindSignatureFromFileAsync(int fileId,
            CancellationToken cancellationToken = default)
        {
            return await _signatureRepo.GetFirstAsync(x => x.FileId == fileId, true, cancellationToken);
        }

        private byte[] ConvertBase64ToBytes(string base64String)
        {
            // Delete prefix "data:image/png;base64," if existed
            var base64Data = base64String.Substring(base64String.IndexOf(',') + 1);
            return Convert.FromBase64String(base64Data);
        }

        private void SignToPdfFile(string filePath, byte[] imageAsBytes)
        {
            // Open the PDF file
            using (var pdfDocument = PdfReader.Open(filePath, PdfDocumentOpenMode.Modify))
            {
                // Get the last page
                var pdfPage = pdfDocument.Pages[pdfDocument.PageCount - 1];
                var gfx = XGraphics.FromPdfPage(pdfPage);

                // Create PDF image from signature image
                using (var stream = new MemoryStream())
                {
                    // Open the signature image 
                    using (var signatureImage = Image.Load<Rgba32>(imageAsBytes))
                    {
                        signatureImage.SaveAsPngAsync(stream);

                        // Reset position
                        stream.Position = 0;

                        var xImage = XImage.FromStream(stream);

                        // Set position and size of the signature image on the PDF page
                        var xPosition = pdfPage.Width.Point - xImage.PixelWidth * 0.75;
                        var yPosition = pdfPage.Height.Point - xImage.PixelHeight * 0.75;

                        gfx.DrawImage(xImage, xPosition, yPosition, xImage.PixelWidth * 0.75, xImage.PixelHeight * 0.75);
                    }

                    pdfDocument.Save(filePath);
                }
            }
        }

        private void SignToWordFile(string filePath, byte[] imageAsBytes)
        {
            // Open the word file
            using (var document = DocX.Load(filePath))
            {
                // Insert signature image to the word file
                using (var ms = new MemoryStream(imageAsBytes))
                {
                    var signatureImage = document.AddImage(ms);
                    var picture = signatureImage.CreatePicture();

                    // Insert the signature image to the new page at the end of document
                    var paragraph = document.InsertParagraph();
                    paragraph.AppendPicture(picture);
                }

                // Save the edited file, overwriting the old file
                document.Save();
            }
        }

        private void SignToImageFile(string filePath, byte[] imageAsBytes)
        {
            // Open the original image file
            using (var originalImage = Image.Load<Rgba32>(filePath))
            {
                // Open the signature image 
                using (var signatureImage = Image.Load<Rgba32>(imageAsBytes))
                {
                    // Set the location to insert the signature image on the original image
                    var location = new Point(50, 50);

                    // Insert/Draw the signature image on the original image
                    originalImage.Mutate(x => x.DrawImage(signatureImage, location, 1f));

                    // Save the edited image, overwriting the old file
                    originalImage.Save(filePath);
                }
            }
        }
    }
}
