<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container text-center">
        <div class="row">
            <div class="col-9">
                <div class="alert alert-primary" role="alert">總共會員數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary" role="alert">??</div>
            </div>
        </div>


        <div class="row">

            <div class="col-4">
                <input type="date" class="form-control text-center" id="date" name="date"><%--////--%>
            </div>
            <div class="col-2">
                <h2 style="text-align: center">~</h2>
            </div>
            <div class="col-4">
                <input type="date" class="form-control " id="date2" name="date"><%--////--%>
            </div>
            <div class="col-2">
                <button type="button" class="btn btn-primary" id="topAdd" style="text-align: center">查詢</button>
            </div>

        </div>
        <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">增加會員數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary">??</div>
            </div>
        </div>
         <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">增加到訪人數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary">??</div>
            </div>
        </div>
        <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">此區間平均會員數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary">??</div>
            </div>
        </div>
        <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">此區間平均到訪人數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary">??</div>
            </div>
        </div>
    </div>


</asp:Content>
