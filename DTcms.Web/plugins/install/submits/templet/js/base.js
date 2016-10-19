/* 

*需要结合jquery和Validform和lhgdialog一起使用
----------------------------------------------------------*/
/*返回顶部*/
var lastScrollY = 0;
$(function(){
	$("body").prepend("<a id=\"gotop\" class=\"gotop\" href=\"#\" title=\"返回顶部\" onfocus=\"this.blur()\" onclick=\"window.scrollTo(0,0);\"></a>");
	window.setInterval("gotop()",1);
});
function gotop(){
	var diffY;
	if (document.documentElement && document.documentElement.scrollTop)
		diffY = document.documentElement.scrollTop;
	else if (document.body)
		diffY = document.body.scrollTop
	else
		{/*Netscape stuff*/}
	percent=.1*(diffY-lastScrollY);
	if(percent>0)percent=Math.ceil(percent);
	else percent=Math.floor(percent);
	lastScrollY=lastScrollY+percent;
	if(lastScrollY<100){
	document.getElementById("gotop").style.display="none";
	} else {
	document.getElementById("gotop").style.display="block";
	}
}
/*搜索查询*/
function SiteSearch(send_url, divTgs, channel_name) {
    var strwhere = "";
    if (channel_name !== undefined) {
        strwhere = "&channel_name=" + channel_name
    }
	var str = $.trim($(divTgs).val());
	if (str.length > 0 && str != "输入关健字") {
	    window.location.href = send_url + "?keyword=" + encodeURI($(divTgs).val()) + strwhere;
	}
	return false;
}
/*切换验证码*/
function ToggleCode(obj, codeurl) {
    $(obj).children("img").eq(0).attr("src", codeurl + "?time=" + Math.random());
	return false;
}
//复制文本
function copyText(txt){
	window.clipboardData.setData("Text",txt); 
	$.dialog.tips("复制成功，可以通过粘贴来发送！",2,"32X32/succ.png");
} 


/*PROPS选择卡特效*/
function ToggleProps(obj, cssname){
	$(obj).parent().children("li").removeClass(cssname);
	$(obj).addClass(cssname);
}
//Tab控制选项卡
function tabs(tabId, event) {
    //绑定事件
	var tabItem = $(tabId + " #tab_head ul li a");
	tabItem.bind(event,function(){
		//设置点击后的切换样式
		tabItem.removeClass("current");
		$(this).addClass("current");
		//设置点击后的切换内容
		var tabNum = tabItem.parent().index($(this).parent());
		$(tabId + " .tab_inner").hide();
        $(tabId + " .tab_inner").eq(tabNum).show();
	});
}
//显示浮动窗口
function showWindow(objId){
	var box = '<div style="text-align:left;line-height:1.8em;">' + $('#' + objId).html() + '</div>';
	var tit = $('#' + objId).attr("title");
	var dialog = $.dialog({
		lock: true,
		min: false,
		max: false,
		resize: false,
		title: tit,
		content: box,
		width: 480,
		ok: function () {
		},
		cancel: false
	});
}

//单击执行AJAX请求操作
function clickSubmit(sendUrl){
	$.ajax({
		type: "POST",
		url: sendUrl,
		dataType: "json",
		timeout: 20000,
		success: function(data, textStatus) {
			if (data.status == 1){
				$.dialog.tips(data.msg, 2, "32X32/succ.png", function(){
					location.reload();
			    });
			} else {
				$.dialog.alert(data.msg);
			}
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			$.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
		}
	});
}



//智能浮动层函数
$.fn.smartFloat = function() {
	var position = function(element) {
		var top = element.position().top, pos = element.css("position");
		var w = element.innerWidth();
		$(window).scroll(function() {
			var scrolls = $(this).scrollTop();
			if (scrolls > top) {
				if (window.XMLHttpRequest) {
					element.css({
						width: w,
						position: "fixed",
						top: 0
					});	
				} else {
					element.css({
						top: scrolls
					});	
				}
			}else {
				element.css({
					position: pos,
					top: top
				});	
			}
		});
	};
	return $(this).each(function() {
		position($(this));						 
	}); 
};

/*表单AJAX提交封装(包含验证)*/
function AjaxInitForm(formId, btnId, isDialog, urlId){
    var formObj = $('#' + formId);
	var btnObj = $("#" + btnId);
	var urlObj = $("#" + urlId);
	formObj.Validform({
		tiptype:3,
		callback:function(form){
			//AJAX提交表单
            $(form).ajaxSubmit({
                beforeSubmit: formRequest,
                success: formResponse,
                error: formError,
                url: formObj.attr("url"),
                type: "post",
                dataType: "json",
                timeout: 60000
            });
            return false;
		}
	});
    
    //表单提交前
    function formRequest(formData, jqForm, options) {
        btnObj.prop("disabled", true);
        btnObj.val("提交中...");
    }

    //表单提交后
    function formResponse(data, textStatus) {
		if (data.status == 1) {
            btnObj.val("提交成功");
			//是否提示，默认不提示
			if(isDialog == 1){
				$.dialog.tips(data.msg, 2, "32X32/succ.png", function(){
					if(data.url){
						location.href = data.url;
					}else if(urlObj.length > 0 && urlObj.val() != ""){
						location.href = urlObj.val();
					}else{
						location.reload();
					}
				});
			}else{
				if(data.url){
					location.href = data.url;
				}else if(urlObj){
					location.href = urlObj.val();
				}else{
					location.reload();
				}
			}
        } else {
            $.dialog.alert(data.msg);
            btnObj.prop("disabled", false);
            btnObj.val("再次提交");
        }
    }
    //表单提交出错
    function formError(XMLHttpRequest, textStatus, errorThrown) {
		$.dialog.alert("状态：" + textStatus + "；出错提示：" + errorThrown);
        btnObj.prop("disabled", false);
        btnObj.val("再次提交");
    }
}

/*显示AJAX分页列表*/
function AjaxPageList(listDiv, pageDiv, pageSize, pageCount, sendUrl, defaultAvatar) {
    //pageIndex -页面索引初始值
    //pageSize -每页显示条数初始化
    //pageCount -取得总页数
	InitComment(0);//初始化评论数据
	$(pageDiv).pagination(pageCount, {
		callback: pageselectCallback,
		prev_text: "« 上一页",
		next_text: "下一页 »",
		items_per_page:pageSize,
		num_display_entries:3,
		current_page:0,
		num_edge_entries:5,
		link_to:"javascript:;"
	});
	
    //分页点击事件
    function pageselectCallback(page_id, jq) {
        InitComment(page_id);
    }
    //请求评论数据
    function InitComment(page_id) {                                
        page_id++;
		$.ajax({ 
            type: "POST",
            dataType: "json",
            url: sendUrl + "&page_size=" + pageSize + "&page_index=" + page_id,
            beforeSend: function (XMLHttpRequest) {
				$(listDiv).html('<p style="line-height:35px;">正在很努力加载，请稍候...</p>');
			},
			success: function(data) {
                //$(listDiv).html(data);
				var strHtml = '';
				for(var i in data){
					strHtml += '<li>' + 
					'<div class="floor">#' + (parseInt(parseInt(i) + 1) + parseInt(pageSize) * parseInt(page_id-1)) + '</div>' +
					'<div class="avatar">';
					if(typeof(data[i].avatar) != "undefined"){
						strHtml += '<img src="' + data[i].avatar + '" width="36" height="36" />';
					}else{
						strHtml += '<img src="' + defaultAvatar + '" width="36" height="36" />';
					}
					strHtml += '</div>' +
					'<div class="inner">' +
					'<p>' + unescape(data[i].content) + '</p>' +
					'<div class="meta">' +
					'<span class="blue">' + data[i].user_name + '</span>\n' +
					'<span class="time">' + data[i].add_time + '</span>' +
					'</div>' +
					'</div>';
					if(data[i].is_reply == 1){
						strHtml += '<div class="answer">' +
						'<div class="meta">' +
						'<span class="right time">' + data[i].reply_time + '</span>' +
						'<span class="blue">管理员回复：</span>' +
						'</div>' + 
						'<p>' + unescape(data[i].reply_content) + '</p>' +
						'</div>';
					}
					strHtml += '</li>';
				}
				$(listDiv).html(strHtml);
            },
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				$(listDiv).html('<p style="line-height:35px;">暂无评论，快来抢沙发吧！</p>');
			}
        });
    }
}