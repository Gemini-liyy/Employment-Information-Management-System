/**
 * Created by dell on 2017/3/21.
 * 招聘信息管理，获取招聘信息
 */
//angularJS获取招聘信息
var app = angular.module("app",[])
    .controller("jobCtrl",["$scope","jobService",function($scope,jobService){
        jobService.getJob(function (data) {
            $scope.jobs = data;
        });
        //获取新添加的招聘信息
        $scope.newJob = {
            post:"",
            xueli:"",
            salary:"",
            address:"",
            experience:"",
            remark:""
        };

        //添加招聘信息

        //jobService.addJob($scope.newJob, function (data) {
        //    alert("添加成功");
        //});

        //删除招聘信息
        $scope.deleteJob = function (delID) {
            //alert(delID);
            jobService.delJob(delID, function (data) {
                alert(data);
                jobService.getJob(function (data) {
                    //console.log(data);
                    $scope.jobs = data;
                });
            });
        };

        //查看详细招聘信息
        jobService.detailJob(function (data) {
            $scope.job = data[0];
            //console.log(data);
        });
    }])
    .factory("jobService", ["$http", "$httpParamSerializer", function ($http, $httpParamSerializer) {
        return{
            //获取所有招聘信息

            getJob:function(handler){
                $http.get("../ashx/getJob.ashx").success(function (data) {
                    handler(data);
                })
            },

            //添加招聘信息

            //addJob: function (params, handler) {
            //    $http({
            //        method: 'post',
            //        url: '../ashx/addJob.ashx',
            //        params: 'params'
            //    }).success(function (data) {
            //        alert(data);
            //    })
            //},

            //删除招聘信息

            delJob: function (id,handler) {
                $http.get("../ashx/delJob.ashx", {
                    params: {
                        "id":id
                    }
                }).success(function (data) {
                    handler(data);
                })
            },
            //查看详细招聘信息
            detailJob: function (handler) {
                var url = window.location.href;
                var id = url.substring(url.lastIndexOf('=') + 1);
                //console.log(id);
                $http.get("../ashx/detailJob.ashx", {
                    params: {
                        "id": id
                    }
                }).success(function (data) {
                    handler(data);
                })
            }
           

        }
    }]);

