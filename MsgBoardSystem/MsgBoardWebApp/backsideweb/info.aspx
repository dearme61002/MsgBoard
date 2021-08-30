<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {

            $("#topbutton").on("click", function () {

                var date = document.getElementById("date").value;
                var date2 = document.getElementById("date2").value;
                
                const obj = {
                    date: date,
                    date2: date2
                };
                data = JSON.stringify(obj);
                $.ajax({

                    type: 'POST',
                    url: 'api/back/Getinfo',

                    data: "=" + data,
                    success: function (res) {
                        //$('#addAllmumber').html = res.AllRegisteredPeople;
                        //$('#addGoHere').html = res.AllPeopleOnline;
                        //$('#avgAllmumber').html = res.avgRegisteredPeople;
                        //$('#avgGoHere').html = res.avgPeopleOnline;
                        
                        document.getElementById("addAllmumber").innerHTML = res.AllRegisteredPeople;
                        document.getElementById("addGoHere").innerHTML = res.AllPeopleOnline;
                        document.getElementById("avgAllmumber").innerHTML = res.avgRegisteredPeople;
                        document.getElementById("avgGoHere").innerHTML = res.avgPeopleOnline;


                        alert(res.msg);
                    },
                    error: function (res) {
                        return alert(res.msg);
                    }

                })
            
            })





        })
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container text-center">
        <div class="row">
            <div class="col-9">
                <div class="alert alert-primary" role="alert">總共會員數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary" role="alert"><%:memberCount %></div>
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
                <button type="button" class="btn btn-primary" id="topbutton" style="text-align: center">查詢</button>
            </div>

        </div>
        <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">增加會員數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary" id="addAllmumber"></div>
            </div>
        </div>
         <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">增加到訪人數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary"id="addGoHere"></div>
            </div>
        </div>
        <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">此區間平均會員數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary" id="avgAllmumber"></div>
            </div>
        </div>
        <div class="row">

            <div class="col-9">
                <div class="alert alert-primary">此區間平均到訪人數</div>
            </div>
            <div class="col-3">
                <div class="alert alert-primary" id="avgGoHere"></div>
            </div>
        </div>
    </div>


</asp:Content>
