/**
 * Created by dell on 2017/3/21.
 * 学生单人个人信息查看
 */
// 跳转至修改个人信息
function product_add(title, url) {
    var index = layer.open({
        type: 2,
        title: title,
        content: url
    });
    layer.full(index);
}

//angularJS获取学生个人信息
var app = angular.module("app",[])
    .controller("personalCtrl",["$scope","studentService",function($scope,studentService){
        studentService.getPersonal(function (data) {
            //console.log(data[0]);
            var data = data[0];
            $scope.personal = data;
        });
        $scope.personalMessage = {
            stu_id:"",
            name:"",
            gender:"",
            age:"",
            nation:"",
            admission:"",
            zhengzhimm:"",
            phone:"",
            birthday:"",
            IDnumber:"",
            Postcode:"",
            shengyuandi:"",
            address:"",
            enterprize:""

        };
        $scope.jump = function () {

        };
        //studentService.updatePersonal(function(data){
        //    alert(data);
        //})
    }])
    .factory("studentService",["$http",function($http){
        return {
            //获取个人信息
            getPersonal:function(handler){
                var url = (parent.location.href);
                var user = url.substring(url.lastIndexOf('=') + 1);
                 //alert(user);
                $http.get("../ashx/getStudent.ashx?stu_id="+user,{
                    stu_id:user
                }).success(function(data){
                    handler(data);
                })
            },
            //updatePersonal:function(params,handler){
            //    // var url = (parent.location.href);
            //    // var user = url.substring(url.lastIndexOf('=') + 1);
            //    $http.get("url",{
            //        data:params
            //        }
            //    ).success(function(data){
            //        handler(data)
            //    })
            //}
        }
    }]);