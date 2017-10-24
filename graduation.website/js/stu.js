/**
 * Created by dell on 2017/3/19.
 * 添加学生信息
 */
var app = angular.module("app",[])
    .controller("stuCtrl",["$scope","studentService",function($scope,studentService){
        studentService.getStudents(function (data) {
            //console.log(data);
            $scope.students = data;
        });
        $scope.newStudent = {
            stu_id: "",
            password:"",
            name:"",
            gender:"",
            nation:"",
            admission:"",
            zhengzhimm:"",
            phone:"",
            birthday:"",
            IDnumber:"",
            Postcode:"",
            shengyuandi:"",
            address:""
        };
        //添加学生信息
        
        //studentService.addStudent($scope.newStudent, function (data) {
        //    alert(data);
        //});
        //删除

        //删除学生信息
        $scope.deleteStudent = function (delID) {
            studentService.delStudent(delID, function (data) {
                alert(data);
                studentService.getStudents(function (data) {
                    $scope.students = data;
                });
            });

        };

        //查看学生详细信息
        studentService.deteilStudent(function (data) {
            $scope.student = data[0];
            console.log(data);
        })
        
    }])
    .factory("studentService",["$http",function($http){
        return{
            //获取所有学生信息
            getStudents:function(handler){
                $http.get("../ashx/students.ashx").success(function (data) {
                    handler(data);
                })
            },
            //添加学生信息

            //addStudent:function(params,handler){
            //    $http.post("url",{
            //        data:params//uid=1&pid=2
            //    }).success(function(data){
            //        handler(data);
            //    })
            //},

            //删除学生信息
            delStudent: function (stu_id,handler) {
                $http.get("../ashx/delStudent.ashx", {
                    params: {
                        "stu_id": stu_id
                    }
                }).success(function (data) {
                    handler(data);
                })
            },

            //查看学生详细信息
            deteilStudent: function (handler) {
                var url = window.location.href;
                var stu_id = url.substring(url.lastIndexOf('=') + 1);
                $http.get("../ashx/detailStudent.ashx", {
                    params: {
                        "stu_id":stu_id
                    }
                }).success(function (data) {
                    handler(data);
                })
            }
        }
    }]);