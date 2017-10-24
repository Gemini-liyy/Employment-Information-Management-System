/**
 * Created by dell on 2017/3/19.
 */
$(function(){
    var url = window.location.href;
    url = unescape(decodeURI(url));
    var user = url.substring(url.lastIndexOf('=') + 1);
     //alert(user);
    if (user == "admin") {
        $("#menu-article").hide();
    } else {
        $.get("../ashx/getStudent.ashx", {
            stu_id: user
        }).success(function (data) {
            var name = JSON.parse(data)[0].name;
            //alert(name);
            //console.log(data);
            $(".userName").text(name);
        }, "json");
        $(".stuHide").hide();
    }
    $.get("../ashx/getStudent.ashx", {
                stu_id: user
            }).success(function (data) {
                var name = JSON.parse(data)[0].name;
                //alert(name);
                //console.log(data);
                $(".userName").text(name);
            }, "json");
})
