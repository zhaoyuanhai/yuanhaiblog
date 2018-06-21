$("#header").removeClass("alt");
baguetteBox.run('.gallery', {
    // 配置参数
    buttons: Boolean,//是否显示导航按钮。
    noScrollbars: true,//是否在显示时隐藏滚动条。
    titleTag: true,//是否使用图片上的title属性作为图片标题
    async: false,//是否异步加载文件。

    afterShow: function () {//显示遮罩层之后的回调函数。   
        $(".pressing").fadeIn();
    },
    afterHide: function () {//隐藏遮罩层之后的回调函数。 
        $(".pressing").fadeOut();
    }
    //preload:5 预加载多少个文件。
    // onChange: function(currentIndex, imagesCount){} 图片改变时的回调函数 
    // overlayBackgroundColor:'rgba(0,0,0,0.8)' 遮罩层的背景颜色
});