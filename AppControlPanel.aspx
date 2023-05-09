<%@ Page Title="" Language="C#" MasterPageFile="~/SiteApp.Master" AutoEventWireup="true" CodeBehind="AppControlPanel.aspx.cs" Inherits="BPWEBAccessControl.AppControlPanel" %>

<%@ MasterType VirtualPath="~/SiteApp.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content w3-padding-16">
     <div class="row">
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
        </div>
        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 text-right">
            <asp:Label ID="lblVwTitle" runat="server" CssClass="label view-title" Text="General Entry"></asp:Label>
        </div>
    </div>
    <hr />
        <div class="row">
        <div class="col-md-12 col-lg-12">
            <asp:MultiView ID="mvwControls" runat="server" ActiveViewIndex="0">
                <%--Generel Entry Vw Start--%>
                <asp:View ID="vwEntry" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                &nbsp;
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                &nbsp;
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                &nbsp;
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </asp:View>
                <%--Generel Entry Vw End--%>
                <%--Report View Start--%>
                <asp:View ID="vwReport" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3" style="text-align: center; vertical-align: middle; border-right: 2px inset #acacac;">
                                <%--<span style="font-family: Calibri; color: darkgray; font-size-adjust: 0.58; font-size: 19pt;">Raw Material Shipment Plan
                                </span>--%>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <%--<div class="panel panel-default myCard">
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="center-block" CommandName="Order/Shipment Schedule Tracking" AlternateText="Order/Shipment Schedule Tracking" ToolTip="Order/Shipment Schedule Tracking"
                                        OnClick="LinkButton_Click"
                                        ImageUrl="~/Depiction/report.png" />
                                    <br />
                                    <asp:Label ID="Label1" runat="server" CssClass="label center-block ctrlpanel-link"
                                        Text="Order/Shipment Schedule Tracking" AssociatedControlID="ImageButton1"></asp:Label>
                                </div>--%>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <%--<div class="panel panel-default myCard">
                                    <asp:ImageButton ID="ImageButton2" runat="server" CssClass="center-block" CommandName="Planning Reports" AlternateText="Planning Reports" ToolTip="Planning Reports"
                                        OnClick="LinkButton_Click"
                                        ImageUrl="~/Depiction/report.png" />
                                    <br />
                                    <asp:Label ID="Label2" runat="server" CssClass="label center-block ctrlpanel-link"
                                        Text="Planning Reports" AssociatedControlID="ImageButton2"></asp:Label>
                                </div>--%>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </asp:View>
                <%--Report View End--%>
                <%--Admin Settings Start--%>
                <asp:View ID="vwAdmin" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3">
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                &nbsp;
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12 col-lg-12">
            <asp:Panel ID="panError" runat="server" CssClass="text-danger" Visible="false">
                <asp:Label ID="TxtMsgBox" runat="server"></asp:Label>
            </asp:Panel>
        </div>
    </div>
    </div>
</asp:Content>
