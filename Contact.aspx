<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BPWEBAccessControl.Contact" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid frmSupportContainer02">

        <div class="row frmSupportContainer02-header">
            <div class="col-md-6 col-sm-6 text-left">
                <%--<asp:Label ID="lblCustIDName" runat="server" Visible="false"></asp:Label>--%>
            </div>
            <div class="col-md-6 col-sm-6 text-right">
                <asp:Label ID="lblfrmName" runat="server">frmName</asp:Label>
            </div>
        </div>

        <div class="row frmSupportContainer02-body">
            <div class="col-md-6">
                <address>
                    BHS Infotech Ltd
        NO. 90A/T, GROUP 6, QUARTER 1A,<br />
                    TAN HIEP WARD, BIEN HOA CITY,
                <br />
                    VIETNAM<br />
                    <abbr title="Phone">P:</abbr>
                    +84969729777
                </address>
                <address>
                    <strong>Support:</strong>   <a href="mailto:BHSAdmin@bhsinfotech.com">Software Support</a><br />
                </address>
                <br />
                <br />
                <p>
                    <asp:LinkButton ID="lblClose" runat="server" CssClass="w3-button" OnClick="lblClose_Click">Close</asp:LinkButton>
                </p>
            </div>
        </div>

        <div class="row frmSupportContainer02-footer">
                <div class="col-md-12 col-sm-12">
                    <div class="row">
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-4">
                            <p class="text-center text-info">
                                <small>Copyright © BHS Infotech 2021. All rights reserved.</small>
                            </p>
                        </div>
                        <div class="col-sm-4">
                        </div>
                    </div>
                </div>
            </div>
    </div>
</asp:Content>
