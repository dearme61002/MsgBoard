<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page01Default.aspx.cs" Inherits="MsgBoardWebApp.Page01Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .AllRangeSetting {
            font-family: 微軟正黑體;
            width: 400px;
            background-color: antiquewhite;
        }

        .auto-style1 {
            font-family: 微軟正黑體;
            width: 1080px;
            background-image: url("../Img/udon.png");
            background-size: contain;
            background-repeat: no-repeat;
            background-position: center;
            background-color: antiquewhite;
            height: 418px;
        }
    </style>

    <table class="auto-style1">
        <tr>
            <td>            
               <h2 id="fade" style="width: 350px; text-align: center;"></h2>
                <script>
                    function fadetext() {
                        fade.style.filter = "alpha(opacity=" + x + ", style=0)";
                        x = ((y < 50) ? x + 4 : x - 4); y += 2;
                        if (y == 100) {
                            z = ((z >= texts.length - 1) ? 0 : z + 1); y = 0;
                            fade.innerHTML = texts[z]; fade.style.color = tcolor[z];
                        }
                        setTimeout("fadetext();", 30);
                    }
                    var texts = new Array(4), tcolor = new Array(4), x = 0, y = 0, z = 0;
                    texts[0] = "歡迎光臨!";
                    texts[1] = "這裡是Woolang留言板!";
                    texts[2] = "不論有任何疑難雜症";
                    texts[3] = "都在這裡不吐不快!!!";
                    tcolor[0] = "red";
                    tcolor[1] = "green";
                    tcolor[2] = "blue";
                    tcolor[3] = "#FF6F00 ";
                    fade.innerHTML = texts[z]; fade.style.color = tcolor[z];
                    window.onload = fadetext;
                </script>
                <a>相關訊息1</a>
                <a>相關訊息2</a>
            </td>
        </tr>
        <tr>
            <td>
                <h2>熱門貼文放這裡123</h2>
                <div>
                    <a href="#">1 貼文標題 發 者 時間</a><br />
                    <a href="#">2 貼文標題 發 者 時間</a><br />
                    <a href="#">3 貼文標題 發 者 時間</a><br />
                    <a href="#">4 貼文標題 發 者 時間</a><br />
                    <a href="#">5 貼文標題 發 者 時間</a><br />
                    <a href="#">6 貼文標題 發 者 時間</a><br />
                    <a href="#">7 貼文標題 發 者 時間</a><br />
                    <a href="#">8 貼文標題 發 者 時間</a><br />
                    <a href="#">9 貼文標題 發 者 時間</a><br />
                    <a href="#">10  貼文標題 發 者 時間</a><br />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
