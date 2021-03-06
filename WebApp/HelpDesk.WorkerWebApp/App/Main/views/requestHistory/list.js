﻿(function () {
    var app = angular.module('app');

    var controllerId = 'app.views.requestHistory.list';
    app.controller(controllerId, [
                 '$rootScope', '$scope', '$state', '$http', '$timeout', '$interval', 'requestService', 'modalService', 'inlineDetailService',
    function ($rootScope, $scope, $state, $http, $timeout, $interval, requestService, modalService, inlineDetailService) {

        var vm = this;
        vm.showAlert = true;

        vm.orderInfo = { propertyName: "Id", asc: false };

        vm.createRequest = function () {
            $state.go("request", { objectId: null, requestId: null, mode: null });
        }

        vm.createRequestByRequest = function (requestId) {
            $state.go("request", { requestId: requestId, objectId: null, mode: 'create' });
        }

        vm.edit = function (requestId) {
            $state.go("request", { requestId: requestId, objectId: null, mode: 'edit' });
        }

        //vm.filter = JSON.parse(localStorage.getItem(controllerId+".filter"))
        vm.filter = {
            Id: null,
            ObjectName: null,
            EmployeeInfo: null,
            DescriptionProblem: null,
            Statuses: [],
            WorkerName: null,
            DateInsert: { Value1: null, Value2: null },
            DateEndPlan: { Value1: null, Value2: null }
        };
        
        extendColumnHeader(vm);

        vm.filter.Archive = false;
        vm.filter.ArchiveYear = 0;
        vm.filter.ArchiveMonth = 0;

        requestService.getRequestFilter().then(function (results) {
            vm.filter.ArchiveMonths = results.data.data.ArchiveMonths;
            vm.filter.ArchiveYears = results.data.data.ArchiveYears;
        }, function (error) {
            $rootScope.$broadcast("error", { errorMsg: error.data.Message });
        });

        
        vm.loaded = false;
        vm.refresh = function () {
            vm.loaded = false;
            //localStorage.setItem(controllerId + ".filter", JSON.stringify(vm.filter));
            requestService.getList(vm.filter, vm.orderInfo, { currentPage: vm.currentPage - 1, pageSize: vm.pageSize }).then(function (results) {
                vm.recs = results.data.data;
                vm.totalCount = results.data.totalCount;
                vm.count = results.data.count;
                vm.loaded = true;
            }, function (error) {
                $rootScope.$broadcast("error", { errorMsg: error.data.Message });
            });

            getStatistics();
        };
   
        vm.refreshFilterStatuses = function (setFilterState, stateId)
        {
            vm.filter.Statuses = [];
            requestService.getListStatus(vm.filter.Archive).then(function (results) {
                vm.filterStatuses = results.data.data;
                if (setFilterState && stateId)
                    setFilterState(stateId);
            }, function (error) {
                $rootScope.$broadcast("error", { errorMsg: error.data.Message });
            });
        }
        

        vm.delete = function (id) {
            requestService.delete(id).then(function (results) {
                vm.errors = {};
                vm.showAlert = true;
                if (results.data.success === false) {
                    vm.errors = results.data.errors;
                }
                else {
                    vm.refresh();
                }

            }, function (error) {
                $rootScope.$broadcast("error", { errorMsg: error.data.Message });
            });
        }

        vm.closeAlert = function () {
            vm.showAlert = false;
        }

        vm.getDeadlineColor = function (r)
        {
            if (r.Archive)
                return "";

            if (r.Expired)
                return "red";

            if (r.Deadline)
                return "blue";

            return "";

        }

        vm.showEvents = function (r) {

            if (!r.openEvents)
                r.openEvents = true;
            else
                r.openEvents = false;

            if (r.openEvents) {
                
                angular.element("#rec" + r.Id).after("<tr id='details" + r.Id + "'></tr>");
                $timeout(function () {

                    inlineDetailService.showDetail({
                        templateUrl: "/AngularTemplate/RequestEvents",
                        controller: "app.views.requestHistory.events as vm",
                        inputs: {
                            params: { requestId: r.Id }
                        },
                        appendElement: angular.element("#details" + r.Id)
                    }).then(function (modal) {

                    });

                }, 100);

                
            }
            else {
                angular.element("#details" + r.Id).remove();
                
            }
        };

        vm.openNewRequestEventForm = function (requestId, statusRequestId, statusName, requestVersion)
        {
            modalService.showModal({
                templateUrl: "/AngularTemplate/RequestEvent?statusRequestId=" + statusRequestId,
                controller: "app.views.requestHistory.event as vm",
                inputs: {
                    params: { requestId: requestId, statusRequestId: statusRequestId, statusName: statusName, requestVersion: requestVersion }
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {

                    if (result.cancel)
                        return;
                    
                    vm.refresh();
                });
            });
        }

        vm.getFilterStateChecked = function (stateId)
        {
            for (var i = 0; i < vm.filterStatuses.length; i++)
            {
                var t = vm.filterStatuses[i];
                if(t.Id == stateId && t.Checked)
                    return true;
            }
            return false;
        }

        var setFilterState = function (stateId)
        {
            for (var i = 0; i < vm.filterStatuses.length; i++) {
                var t = vm.filterStatuses[i];
                if (t.Id == stateId)
                    t.Checked = !t.Checked;
            }

            vm.applyFilter('Statuses');
        }

        vm.setFilterState = function (stateId)
        {
            if (vm.filter.Archive) {
                vm.filter.Archive = false;
                vm.refreshFilterStatuses(setFilterState, stateId);
                return;
            }
                       
            setFilterState(stateId);
        }

        var getStatistics = function ()
        {
            requestService.getListRequestStateCount().then(function (results) {
                vm.requestStateCounts = results.data.data;
            }, function (error) {
                $rootScope.$broadcast("error", { errorMsg: error.data ? error.data.Message : "Нет связи с сервером" });
            });
        }
        
        //$interval(getStatistics, 5000);
        
        vm.refreshFilterStatuses();
        vm.refresh();

    }

    ]);
})();