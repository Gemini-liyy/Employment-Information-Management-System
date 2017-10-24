/**
 * Created by dell on 2017/3/21..
 * 添加就业信息
 */
//angularJS获取学生就业信息
var app = angular.module("app",[])
    .controller("employmentCtrl",["$scope","employmentService",function($scope,employmentService){
        employmentService.getEmployment(function (data) {
            $scope.employments = data;
        });
        $scope.newEmployment = {
            stu_id:"",
            name:"",
            jiuzhishijian:"",
            qiye_name:"",
            jiuzhigangwei:"",
            salary:"",
            remark:""
        };
        //添加就业信息
        
        //employmentService.addEmployment($scope.newEmployment,function(data){
        //    alert(data)
        //});

        //删除就业信息

        $scope.deleteEmployment = function (delID) {
            //alert(delID);
            employmentService.delEmployment(delID, function (data) {
                alert(data);
                employmentService.getEmployment(function (data) {
                    $scope.employments = data;
                });
            });
            
        };

        //查看详细就业信息
        employmentService.detailEmployment(function (data) {
            $scope.employment = data[0];
            console.log(data);
        })

    }])
    .factory("employmentService",["$http",function($http){
        return{
            //获取所有就业信息
            getEmployment:function(handler){
                    $http.get("../ashx/getEmployment.ashx").success(function (data) {
                    handler(data);
                })
            },
            //添加就业信息
            
            //addEmployment:function(params,handler){
            //    $http.post("",{
            //        data:params
            //    }).success(function(data){
            //        handler(data);
            //    })
            //}

            //删除就业信息

            delEmployment: function (id,handler) {
                $http.get("../ashx/delEmployment.ashx", {
                    params: {
                        "id": id
                    }
                }).success(function (data) {
                    handler(data);
                })
            },

            //查看详细就业信息
            detailEmployment: function (handler) {
                var url = window.location.href;
                var id = url.substring(url.lastIndexOf('=') + 1);
                $http.get("../ashx/detailEmployment.ashx", {
                    params: {
                        "id": id
                    }
                }).success(function (data) {
                    handler(data);
                })
            }


        }
    }]);