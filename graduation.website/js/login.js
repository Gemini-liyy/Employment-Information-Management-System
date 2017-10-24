$(function(){
    //登录信息
    $(".login").click(function () {
        var user = $(".user").val();
        var password = $(".password").val();
        var type = $(".type option:selected").val();
        if (user != "" && password != "" && type != "") {
            if (user == "admin" && password == "admin" && type == "admin") {
                window.location.href = "http://localhost:54257/index.html?" + "user=" + user;
            } else {
                $.get("../ashx/login.ashx", {
                    "stu_id": user,
                    "password": password
                }).success(function (data) {
                    if (data == "[]") {
                        alert("账号或密码错误");
                    } else {
                        console.log(data);
                        window.location.href = "http://localhost:54257/index.html?" + "user=" + user;
                    }
                });
           }   
        }else{
            alert("请输入账号和密码")
        }
    });
});