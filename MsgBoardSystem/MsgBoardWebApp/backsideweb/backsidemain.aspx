<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="backsidemain.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.backsidemain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>後台管理介面</title>
    <link href="../backsideUI/themes/default/easyui.css" rel="stylesheet" />
    <link href="../backsideUI/themes/icon.css" rel="stylesheet" />
    <script src="../backsideUI/jquery.min.js"></script>
    <script src="../backsideUI/jquery.easyui.min.js"></script>
    <script src="../backsideUI/easyui-lang-zh_TW.js"></script>
    <link href="../Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<%--寫功能--%>
	<script>
        $(function () {
            /*綁定按鈕*/
            $('.functionbar').bind('click', function () {

                var title = $(this).text();
                var isexist = $('#mytabs').tabs('exists', title);
                var myurl = $(this).attr("url");
                /*檢查是否出現重複*/
                if (isexist) {
                    $('#mytabs').tabs('select', title);
                    return;
                }
                /*檢查是否出現重複*/
                $('#mytabs').tabs('add', {
                    title: title,
                    content: showContent(myurl),
                    closable: true
                });
            });
            function showContent(myurl) {
                var urlhtml = '<iframe src="' + myurl + '"style="height:100%; width:100%;border:0"></iframe>';
                return urlhtml;
            }


          //刪除cookie
            function delCookie(name) {
                var exp = new Date();
                exp.setTime(exp.getTime() - 1);
                var cval = getCookie(name);
                if (cval != null)
                    document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
            }

            function getCookie(name) {
                let arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
                if (arr != null) return unescape(arr[2]);
                return null;
            }

            /*綁定按鈕*/
            $('#out').bind('click', function () {
                delCookie('.ASPXAUTH');
                parent.window.location.assign("../Page01Default.aspx");
 document.cookie;
            })

  
           


        })

    </script>
	<%--寫功能--%>
</head>
<body class="easyui-layout">
   <%--top--%>
	<div data-options="region:'north',border:false" style="height:100px;padding:10px;overflow:hidden">
          <img src="../Img/Logo.png" width="100" height="100" style="padding-bottom:12px;float:left"/>
        <div style="float:left;line-height:100px;margin-left:20px;font-size:60px">Woolong留言板</div>
		<button type="button" class="btn btn-secondary" id="out" style="float:right">登出</button>
 <%--top--%>
	</div>
	<div data-options="region:'west',split:true,title:'功能列表'" style="width:300px;padding:10px;">
       <%-- 功能列--%>
        <div class="easyui-accordion" data-options="fit:true" style="width:auto;">
	   <a id="index"  href="#" class="easyui-linkbutton functionbar" data-options="iconCls:'icon-reload'" style="width:inherit" url="index.aspx?title=功能首頁" >功能首頁</a> <%--主頁按鈕--%>
				<a id="info"  href="#" class="easyui-linkbutton functionbar" data-options="iconCls:'icon-reload'" style="width:inherit" url="info.aspx?title=統計資訊" >統計資訊</a> <%--統計資訊--%>
		<div title="留言貼文管理"  data-options="iconCls:'icon-ok',fit:true" style="padding:10px 0px;width:auto;">
	   <a id="badlanguage" href="#" class="easyui-linkbutton functionbar"  style="width:inherit" url="badlanguage.aspx?title=禁言管理">禁言管理</a> <%--禁言管理按鈕--%>
			
	    <a id="editarticles" href="#" class="easyui-linkbutton functionbar" data-options="" style="width:inherit" url="editarticles.aspx?title=貼文管理">貼文管理</a> <%--貼文管理按鈕--%>
		<a id="board" href="#" class="easyui-linkbutton functionbar" data-options="" style="width:inherit" url="board.aspx?title=佈告欄管理"">佈告欄管理</a> <%--佈告欄管理管理按鈕--%>
		</div>
		<div title="會員管理" data-options="iconCls:'icon-ok',fit:true"  style="padding:10px 0px;width:auto;">
		<a id="editmember" href="#" class="easyui-linkbutton functionbar" data-options="" style="width:inherit" url="editmember.aspx?title=會員資訊">會員資訊</a> <%--會員資訊按鈕--%>
			<a id="blackmember" href="#" class="easyui-linkbutton functionbar" data-options="" style="width:inherit" url="blackmember.aspx?title=黑名單">黑名單</a> <%--黑名單按鈕--%>	
		</div>
			<div title="系統管理" data-options="iconCls:'icon-ok',selected:true" style="padding:10px 0px;width:auto;">
		<a id="errorlog" href="#" class="easyui-linkbutton functionbar" data-options="" style="width:inherit" url="errorlog.aspx?title=錯誤ＬＯＧ檔案生成">錯誤ＬＯＧ檔案生成</a> <%--錯誤ＬＯＧ檔案生成--%>	
				<a id="passwordchange" href="#" class="easyui-linkbutton functionbar" data-options="" style="width:inherit" url="passwordchange.aspx?title=跟改密碼及帳號">跟改密碼及帳號</a> <%--跟改密碼及帳號--%>	
		</div>
		
		
	</div>
        <%--功能列--%>
	</div>	
	<div data-options="region:'south',border:false" style="height:15px;background:#A9FACD;overflow:hidden;text-align:center;line-height:15px">Copyright © 2021 隨便 Inc.</div>
	<%--主要顯示列--%>
	<div data-options="region:'center',title:'訊息顯示區'">
		<div class="easyui-tabs" style="width:700px;height:250px" fit="true" id="mytabs" >
			<%--內嵌內容--%>
	
			<%--內嵌內容--%>
		
	</div>


	
		
	</div>
	<%--主要顯示列--%>
</body>
</html>
