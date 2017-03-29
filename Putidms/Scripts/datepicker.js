$(".form-datetime").datetimepicker({
    minView: "month", //选择日期后，不会再跳转去选择时分秒 
    format: "yyyy-mm-dd", //选择日期后，文本框显示的日期格式 
    language: 'zh-CN', //汉化 
    autoclose: true //选择日期后自动关闭 
});