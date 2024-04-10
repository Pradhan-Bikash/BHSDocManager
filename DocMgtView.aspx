<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteApp.master" CodeBehind="DocMgtView.aspx.cs" Inherits="BPWEBAccessControl.DocMgtView" %>

<%@ MasterType VirtualPath="~/SiteApp.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid frmbaseContainer02" id="dataUpdatePanel" runat="server">
               <div class="row frmbaseContainer02-header">
                   <div class="col-sm-12 text-right">
                       <asp:Label ID="lblfrmName" runat="server">frmName</asp:Label>
                   </div>
               </div>

                <p>welcome</p>





                </div>
                </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Content>