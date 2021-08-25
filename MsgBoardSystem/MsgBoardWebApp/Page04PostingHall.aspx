<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page04PostingHall.aspx.cs" Inherits="MsgBoardWebApp.Page04PostingHall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Test Page</h1>
    <form runat="server" id="page04form">
        <asp:Literal runat="server" ID="ltlMsg"></asp:Literal><br />
    </form>
    <div class="list-group">
        <a href="#" class="list-group-item list-group-item-action list-group-item-info" aria-current="true">
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">貼文標題1</h5>
                <small class="text-muted">3 days ago(時間點)</small>
            </div>
            <p class="mb-1">一些內容放這裡....</p>
            <small>第二段放這裡...</small>
        </a>
        <a href="#" class="list-group-item list-group-item-action list-group-item-info">
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">貼文標題2</h5>
                <small class="text-muted">3 days ago(時間點)</small>
            </div>
            <p class="mb-1">一些內容放這裡....</p>
            <small class="text-muted">第二段放這裡...</small>
        </a>
        <a href="#" class="list-group-item list-group-item-action list-group-item-info">
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">貼文標題3</h5>
                <small class="text-muted">3 days ago(時間點)</small>
            </div>
            <p class="mb-1">一些內容放這裡....</p>
            <small class="text-muted">第二段放這裡...</small>
        </a>
    </div>
</asp:Content>
