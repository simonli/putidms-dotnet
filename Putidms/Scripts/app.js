
function GetDepartmentList() {
    var divisionId = $("#DivisionId").val();//取出DivisionId
    $.ajax({
        url: "/Department/GetDepartmentList",
        type: "POST",
        data: { "divisionId": divisionId },
        success: function (res) {           
            var htmldata = "<option value=''>-- 请选择修学点 --</option>";
            $.each(res, function (i, item) {
                htmldata = htmldata + "<option value='" + item.Id + "'>" + item.Name + "</option>";
            });            
            $("#DepartmentId").html(htmldata);
        },
    });
}




function GetKlassList() {
    var departmentId = $("#DepartmentId").val();//取出DepartmentId
    $.ajax({
        url: "/Klass/GetKlasstList",
        type: "POST",
        data: { "departmentId": departmentId },
        success: function (res) {
            console.log('#######################')
            console.log(res)
            console.log('@@@@@@@@@@@@@@@@@@@@@@@')
            var htmldata = "<option value=''>-- 请选择班级 --</option>";
            $.each(res, function (i, item) {
                htmldata = htmldata + "<option value='" + item.Id + "'>" + item.Name + "</option>";
            });
            console.log(htmldata);
            $("#KlassId").html(htmldata);
        },
    });
}

function confirm_delete(url) {
    $.confirm({
        title: '⚠️ 警告',
        content: '此操作不可恢复，确认删除吗？',
        type: 'red',
        typeAnimated: true,
        buttons: {
            confirm: {
                text: '删除',
                btnClass: 'btn-danger',
                action: function () {
                    location.href = url;
                }
            },
            cancel: {
                text: '取消',
                btnClass: 'btn-default',
                action: function () {
                }
            }
        }
    });
}
