<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        span {
  position: absolute;
  left: 50%;
  top: 100px;  
  transform: translate(-50%, -50%);
  display: block;
  color: #cf1b1b;
  font-size: 50px;  /*文字大小*/
  letter-spacing: 8px;
  cursor: pointer;
}

span::before {
  content: "設定首頁熱門標題";  /*設定文字*/
  position: absolute;
  color: transparent;
  background-image: repeating-linear-gradient(
    45deg,
    transparent 0,
    transparent 2px,
    white 2px,
    white 4px
  );
  -webkit-background-clip: text;
  top: 0px;
  left: 0;
  z-index: -1;
  transition: 1s;
}

span::after {
  content: "設定首頁熱門標題";/*設定文字*/
  position: absolute;
  color: transparent;
  background-image: repeating-linear-gradient(
    135deg,
    transparent 0,
    transparent 2px,
    white 2px,
    white 4px
  );
  -webkit-background-clip: text;
  top: 0px;
  left: 0px;
  transition: 1s;
}

span:hover:before {
  top: 10px;
  left: 10px;
}

span:hover:after {
  top: -10px;
  left: -10px;
} 
    </style>
</asp:Content>
 <%--接收標題--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
             <%--設定標題--%>
     <span>設定首頁熱門標題</span>
    <%--設定標題--%>
              
</asp:Content>

