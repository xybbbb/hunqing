var arr = new Array();
var path = "";
$("img").each(function() {
    arr.push($(this).attr("src"));
    for (var i=0;i<arr.length;i++) {
    	$(this).attr("src",path+arr[i])
    }
});