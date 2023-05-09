<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebFrmLogIn.aspx.cs" Inherits="PWOMS.WebFrmLogIn" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">

        <div class="col-md-6">
            <div id="ablock">
                <div class="login-form-leftpicContainer LeftContainer-image2 w3-opacity-min">
                    <img src="Picture/A004.png" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div id="bblock">
                <div class="row">
                    <div class="col-md-12">
                        <div id="dataUpdatePanel" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="login-form">
                                        <div class="login-form-header">
                                            <h3>
                                                <asp:Label ID="lblLogIn" runat="server" CssClass="ctrlStyle_Label" Text="Log-In"></asp:Label>
                                            </h3>
                                        </div>

                                        <div id="logInCtrls" runat="server" class="login-form-body">
                                            <div class="form-group">
                                                <asp:Label ID="lblUserId" runat="server" Style="color: #2485FF;" Text="User Id"></asp:Label>
                                                <asp:TextBox ID="TextUserID" runat="server" CssClass="form-control underline-only-ctrl" placeholder="User ID"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblPassword" runat="server" CssClass="ctrlStyle_Label" Style="color: #2485FF;"
                                                    Text="Password"></asp:Label>
                                                <asp:TextBox ID="TextPWD" runat="server" CssClass="form-control underline-only-ctrl" TextMode="Password" placeholder="Password"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblSite" runat="server" CssClass="ctrlStyle_Label" Style="color: #2485FF;"
                                                    Text="Site"></asp:Label>
                                                <asp:DropDownList ID="ddlSite" runat="server" CssClass="form-control underline-only-ctrl"></asp:DropDownList>
                                            </div>
                                            <asp:LinkButton ID="btnLogIn"
                                                runat="server"
                                                CssClass="btn btn-primary" OnClick="btnLogIn_Click">
                                <span aria-hidden="true" class="glyphicon glyphicon-log-in"></span>&nbsp;Log In
                                            </asp:LinkButton>

                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <%--Msg Board Start--%>

    <div id="DivMsgBoard" runat="server" class="container-fluid myMsgBoxStyleContainer">
        <div class="myMsgBoxStyle myMsgBoxStyle-red">
            <div class="myMsgBoxStyle-header myMsgBoxStyle-header-red">
                <p><strong>Message</strong></p>
            </div>
            <div class="myMsgBoxStyle-body myMsgBoxStyle-body-black">
                <asp:Label ID="dlgMsg" CssClass="card-text" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Button ID="dlgOk" CssClass="btn btn-default" runat="server" Text="OK" OnClick="dlgOk_Click" />
            </div>
        </div>
    </div>
    <%--Msg Board End--%>
</asp:Content>
