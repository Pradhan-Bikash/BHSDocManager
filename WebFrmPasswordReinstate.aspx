<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebFrmPasswordReinstate.aspx.cs" Inherits="BPWEBAccessControl.WebFrmPasswordReinstate" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xl-3 col-lg-3 col-md-3"></div>
        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
            <div class="row frmSupportContainer02">
                <div class="col-md-12">
                    <div class="row frmSupportContainer02-header">
                        <div class="col-md-6 col-sm-6 text-left">
                            <h5>
                                <asp:Label ID="lblAppLink" runat="server"></asp:Label>
                            </h5>
                        </div>
                        <div class="col-md-6 col-sm-6 text-right">
                            <h5>(Reset Password)</h5>
                        </div>
                    </div>
                    <div class="row frmSupportContainer02-body">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
                                    <span for="TextUserID">User ID</span>
                                    <asp:TextBox ID="TextUserID" runat="server" CssClass="form-control" placeholder="User ID"></asp:TextBox>
                                </div>
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
                                    <span for="TextPWD">Existing Password</span>
                                    <asp:TextBox ID="TextPWD" runat="server" CssClass="form-control" TextMode="Password" placeholder="Existing Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                             <div class="row">
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
                                    <span for="tbNewPassword">New Password</span>
                                    <asp:TextBox ID="tbNewPassword" runat="server" CssClass="form-control pr-password" TextMode="Password" placeholder="New Password"></asp:TextBox>
                                </div>
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
                                    <span for="tbNewPassword">Re-Enter New Password</span>
                                    <asp:TextBox ID="tbVerifyPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm New Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="row">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 bg-warning font-small" style="color: #1b1b1b">
                                    Password is case - sensitive. It must contain at least - 1 Special Character; 1 Upper-Case Alpha Character; 1 Lower-Case Alpha Character; 1 Numeric Character ; Do not use space
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="row">
                                <asp:Panel ID="panInfo" runat="server" CssClass="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                    <span for="tbEMail" class="required-after">e-Mail ID</span>
                                    <asp:TextBox ID="tbEMail" runat="server" CssClass="form-control"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="clearfix"></div>
                            <div class="row">
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-12 space-vertical">&nbsp;</div>
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-12 space-vertical">&nbsp;</div>
                                <div class="col-xl-4 col-lg-4 col-md-4 col-sm-12 space-vertical">
                                    <br />
                                    <asp:LinkButton ID="btnResetPasswd" runat="server" OnClick="btnResetPasswd_Click" CssClass="btn btn-primary"><span aria-hidden="true" class="glyphicon glyphicon-log-in"></span>&nbsp;Reset Password
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="row">
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                    <asp:Panel ID="panError" runat="server" CssClass="text-danger" Visible="false">
                                        <asp:Label ID="TxtMsgBox" runat="server" Font-Bold="true" Style="word-wrap: break-word;"></asp:Label>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-lg-3 col-md-3"></div>
    </div>
    <script>
        $(function () {
            $(".pr-password").passwordRequirements();
        });
    </script>
</asp:Content>
