function setDotNetHepler(obj) {
    window.dotNetHelper = obj;
}

function initTimelineByLocation(elementId, canScheduleStaff, settings) {
    var timelineElement = document.getElementById(elementId);

    if (timelineElement) {
        // Reset the timeline instance if it already exists
        if (timelineElement.timelineInstance) {
            timelineElement.timelineInstance.destroy();
            timelineElement.timelineInstance = null;
        }

        // Initialize the timeline instance
        timelineElement.timelineInstance = new EventCalendar(timelineElement, {
            view: 'resourceTimelineWeek',
            views: {
                resourceTimelineWeek: {
                    slotDuration: settings.slotDuration,
                    slotMinTime: settings.slotMinTime,
                    slotMaxTime: settings.slotMaxTime,
                    eventStartEditable: canScheduleStaff,
                    eventDurationEditable: canScheduleStaff,
                    slotWidth: settings.slotWidth,
                },
                resourceTimelineMonth: {
                    eventStartEditable: canScheduleStaff,
                    eventDurationEditable: false,
                }
            },
            headerToolbar: {
                start: canScheduleStaff ? 'prev,next today manualScheduleButton' : 'prev,next today',
                center: 'title',
                end: 'resourceTimelineWeek,resourceTimelineMonth'
            },
            buttonText: {
                resourceTimelineWeek: 'Week',
                resourceTimelineMonth: 'Month',
                today: 'Today'
            },
            customButtons: {
                manualScheduleButton: {
                    text: 'Manual Schedule',
                    click: function () {
                        window.dotNetHelper.invokeMethodAsync('OnManualScheduleButtonClick');
                    }
                }
            },
            selectable: canScheduleStaff,
            hiddenDays: [],
            resources: [],
            eventSources: [
                {
                    events: function (info, successCallback, failureCallback) {
                        window.dotNetHelper.invokeMethodAsync('OnGetTimelineLocationEvents', info.startStr, info.endStr)
                            .then(function (events) {
                                successCallback(events);
                            })
                            .catch(function (error) {
                                console.log(error);
                                failureCallback(error);
                            });
                    }
                }
            ],
            select: function (event) {
                var locationId = event.resource.id;
                var today = moment().format('YYYY-MM-DD');
                var fromDate = moment(event.start).format('YYYY-MM-DD');
                var toDate = event.view.type === 'resourceTimelineWeek'
                    ? moment(event.end).format('YYYY-MM-DD')
                    : moment(event.end).add(-1, 'days').format('YYYY-MM-DD');

                var startTime = moment(event.start).format('HH:mm:ss');
                var endTime = moment(event.end).format('HH:mm:ss');

                if (locationId <= 0) {
                    window.dotNetHelper.invokeMethodAsync('OnNotifyError', 'Could not schedule for time off.');
                    timelineElement.timelineInstance.unselect();
                    return;
                }

                if (today > fromDate) {
                    window.dotNetHelper.invokeMethodAsync('OnNotifyError', 'Could not schedule for the past date.');
                    timelineElement.timelineInstance.unselect();
                    return;
                }

                if (event.view.type === 'resourceTimelineWeek') {
                    if (fromDate !== toDate) {
                        window.dotNetHelper.invokeMethodAsync('OnNotifyError', 'Please schedule for one day in Week view.If you want to schedule for multiple days, switch to Month view or click the Manual Schedule button.');
                        timelineElement.timelineInstance.unselect();
                        return;
                    }
                    window.dotNetHelper.invokeMethodAsync('OnSelect', locationId, fromDate, null, startTime, endTime)
                        .catch(function (error) {
                            console.log(error);
                            timelineElement.timelineInstance.unselect();
                        });
                }
                else {
                    window.dotNetHelper.invokeMethodAsync('OnSelect', locationId, fromDate, toDate, null, null)
                        .catch(function (error) {
                            console.log(error);
                            timelineElement.timelineInstance.unselect();
                        });
                }

            },
            eventContent: function (info) {
                var calendarDate = moment(info.event.start).format('MM/DD/YYY');
                var startTime = moment(info.event.start).format('HH:mm');
                var endTime = moment(info.event.end).format('HH:mm');
                var title = info.event.title ?? '';
                var jobRole = info.event.extendedProps.jobRole ?? '';
                var timeOffType = info.event.extendedProps.timeOffType ?? '';

                var content = info.view.type === 'resourceTimelineWeek'
                    ? `<time class="ec-event-time" datetime="${moment(info.event.start).format()}">${startTime}</time>`
                    + `-<time class="ec-event-time" datetime="${moment(info.event.end).format()}">${endTime}</time>`
                    + `<h4 class="ec-event-title">${title} (${jobRole}) ${timeOffType}</h4>`
                    : `<div class="rz-text-truncate">`
                    + `<time class="ec-event-time event-start-time" datetime="${moment(info.event.start).format()}">${startTime}</time>`
                    + `-<time class="ec-event-time event-end-time" datetime="${moment(info.event.end).format()}">${endTime}</time>`
                    + `<h4 class="ec-event-title">${title}</h4>`
                    + `</div>`;

                return {
                    html: content
                };
            },
            eventDidMount: function (info) {
                //var id = info.event.id;
                //var calendarDate = moment(info.event.start).format('MM/DD/YYYY');
                //var startTime = moment(info.event.start).format('HH:mm');
                //var endTime = moment(info.event.end).format('HH:mm');
                //var locationId = info.event.resourceIds[0];
                //// var locationName = locationResources.find(x => x.id == locationId).title;
                //var locationName = 'Location Name';
                //var title = info.event.title ?? '';
                //var jobRole = info.event.extendedProps.jobRole ?? '';
                //var notes = info.event.extendedProps.notes ?? '';
                //var timeOffType = info.event.extendedProps.timeOffType ?? '';

                //var isClinicClosed = info.event.extendedProps.isClinicClosed;
                //var isHoliday = info.event.extendedProps.isHoliday;
                //var isTimeOff = info.event.extendedProps.isTimeOff;

                //var popoverContent = `<div>`
                //    + (isTimeOff ? `<div>Time Off Type: ${timeOffType}</div>` : `<div>Location: ${locationName}</div>`)
                //    + (isHoliday || isClinicClosed
                //        ? `<div>Event: ${title}</div>`
                //        : `<div>Employee: ${title}</div><div>Role: ${jobRole}</div>`)
                //    + `<div>Notes: ${notes}</div>`
                //    + (isClinicClosed || isHoliday || isTimeOff
                //        ? ``
                //        : `<div>`
                //        + `<a href="#" class="btn btn-xs btn-flat btn-info" data-ajax-url="/Timetable/EventHistory/${id}" `
                //        + `data-id="${id}" data-toggle="modal" data-backdrop="static" data-target="#event-history-dialog" data-title="Change history of ${title}'s schedule" `
                //        + `data-bs-html="true" title="View change history of of ${title}'s schedule"><i class="fas fa-receipt"></i> Change History</a>`
                //        + `</div>`)
                //    + `</div>`;

                //$(info.el).popover({
                //    title: `${calendarDate} ${startTime} - ${endTime}`,
                //    placement: 'auto',
                //    animation: true,
                //    delay: { "show": 300, "hide": 2000 },
                //    html: true,
                //    sanitize: false,
                //    content: popoverContent,
                //    trigger: 'hover'
                //});
            },
            eventClick: function (info) {
                var eventType = info.event.extendedProps.eventType;

                if (eventType === "ClinicClosed" || eventType === "Holiday" || eventType === "TimeOff" ) {
                    return;
                }

                var today = moment().format('YYYY-MM-DD');
                var fromDate = moment(info.event.start).format('YYYY-MM-DD');

                if (today > fromDate) {
                    window.dotNetHelper.invokeMethodAsync('OnNotifyError', 'Could not schedule for the past date.');
                    return;
                }

                window.dotNetHelper.invokeMethodAsync('OnEventClick', info.event.id)
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            eventResize: function (info) {
                var id = info.event.id;
                var locationId = info.event.resourceIds[0];
                var employeeId = info.event.extendedProps.employeeId;
                var notes = info.event.extendedProps.notes;

                var today = moment().format('YYYY-MM-DD');
                var fromDate = moment(info.event.start).format('YYYY-MM-DD');
                var toDate = moment(info.event.end).format('YYYY-MM-DD');

                var startTime = moment(info.event.start).format('HH:mm:ss');
                var endTime = moment(info.event.end).format('HH:mm:ss');

                if (today > fromDate) {
                    window.dotNetHelper.invokeMethodAsync('OnNotifyError', 'Could not schedule for the past date.');
                    info.revert();
                    return;
                }

                window.dotNetHelper.invokeMethodAsync('OnEventDrop', id, locationId, employeeId, fromDate, toDate, startTime, endTime, notes)
                    .catch(function (error) {
                        console.log(error);
                        info.revert();
                    });
            },
            eventDrop: function (info) {
                var id = info.event.id;
                var locationId = info.event.resourceIds[0];
                var employeeId = info.event.extendedProps.employeeId;
                var notes = info.event.extendedProps.notes;

                var today = moment().format('YYYY-MM-DD');
                var fromDate = moment(info.event.start).format('YYYY-MM-DD');
                var toDate = moment(info.event.end).format('YYYY-MM-DD');

                var startTime = moment(info.event.start).format('HH:mm:ss');
                var endTime = moment(info.event.end).format('HH:mm:ss');

                if (locationId <= 0) {
                    window.dotNetHelper.invokeMethodAsync('OnNotifyError', 'Could not schedule for time off.');
                    info.revert();
                    return;
                }

                if (today > fromDate) {
                    window.dotNetHelper.invokeMethodAsync('OnNotifyError', 'Could not schedule for the past date.');
                    info.revert();
                    return;
                }

                window.dotNetHelper.invokeMethodAsync('OnEventDrop', id, locationId, employeeId, fromDate, toDate, startTime, endTime, notes)
                    .catch(function (error) {
                        console.log(error);
                        info.revert();
                    });
            }
        });
    }
}

function initTimelineByEmployee(elementId, canScheduleStaff, settings) {
    var timelineElement = document.getElementById(elementId);

    if (timelineElement) {
        // Reset the timeline instance if it already exists
        if (timelineElement.timelineInstance) {
            timelineElement.timelineInstance.destroy();
            timelineElement.timelineInstance = null;
        }

        // Initialize the timeline instance
        timelineElement.timelineInstance = new EventCalendar(timelineElement, {
            view: 'resourceTimelineWeek',
            views: {
                resourceTimelineWeek: {
                    slotDuration: settings.slotDuration,
                    slotMinTime: settings.slotMinTime,
                    slotMaxTime: settings.slotMaxTime,
                    slotWidth: settings.slotWidth,
                },
                resourceTimelineMonth: {
                }
            },
            headerToolbar: {
                start: 'prev,next today',
                center: 'title',
                end: 'resourceTimelineWeek,resourceTimelineMonth'
            },
            buttonText: {
                resourceTimelineWeek: 'Week',
                resourceTimelineMonth: 'Month',
                today: 'Today'
            },
            selectable: false,
            eventStartEditable: false,
            eventDurationEditable: false,
            hiddenDays: [],
            resources: [],
            eventSources: [
                {
                    events: function (fetchInfo, successCallback, failureCallback) {
                        window.dotNetHelper.invokeMethodAsync('OnGetTimelineEmployeeEvents', fetchInfo.startStr, fetchInfo.endStr)
                            .then(function (events) {
                                successCallback(events);
                            })
                            .catch(function (error) {
                                console.log(error);
                                failureCallback(error);
                            });
                    }
                }
            ],
            eventContent: function (info) {
                var calendarDate = moment(info.event.start).format('MM/DD/YYY');
                var startTime = moment(info.event.start).format('HH:mm');
                var endTime = moment(info.event.end).format('HH:mm');
                var location = info.event.title ?? '';
                var jobRole = info.event.extendedProps.jobRole ?? '';

                var content = info.view.type === 'resourceTimelineWeek'
                    ? `<time class="ec-event-time" datetime="${moment(info.event.start).format()}">${startTime}</time>`
                    + `-<time class="ec-event-time" datetime="${moment(info.event.end).format()}">${endTime}</time>`
                    + `<h4 class="ec-event-title">${location}</h4>`
                    : `<div class="rz-text-truncate">`
                    + `<time class="ec-event-time" datetime="${moment(info.event.start).format()}">${startTime}</time>`
                    + `-<time class="ec-event-time" datetime="${moment(info.event.end).format()}">${endTime}</time>`
                    + `<h4 class="ec-event-title">${location}</h4>`
                    + `</div>`;

                return {
                    html: content
                };
            },
        });
    }
}

function updateTimelineHiddenDays(elementId, hiddenDays) {
    var timelineElement = document.getElementById(elementId);

    if (timelineElement && timelineElement.timelineInstance) {
        timelineElement.timelineInstance.setOption('hiddenDays', hiddenDays);
    }
}

function updateTimelineResources(elementId, resources) {
    var timelineElement = document.getElementById(elementId);

    if (timelineElement && timelineElement.timelineInstance) {
        timelineElement.timelineInstance.setOption('resources', resources);
    }
}

function refreshTimelineEvents(elementId) {
    var timelineElement = document.getElementById(elementId);

    if (timelineElement && timelineElement.timelineInstance) {
        timelineElement.timelineInstance.refetchEvents();
    }
}