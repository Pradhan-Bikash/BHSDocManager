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

               <!-- Side Navbar -->
              <div class="container-fluid">
    <div class="row g-0 text-center">
        <!-- Left Sidebar -->
       <div class="col-sm-6 col-md-3">
    <div class="sidebar_wrapper">
        <div class="sideBar sticky">
            <div class="sideBar--wrap newLeftbar">
                <style>
                    /* Custom CSS for left sidebar */
                    .sidebar_wrapper {
                        height: calc(100vh - 60px); /* Height of the sidebar (adjust as needed) */
                        overflow-x: auto; /* Enable vertical scrolling */
                        background-color: #f8f9fa; /* Light background color */
                        padding: 0px; /* Padding inside the sidebar */
                    }
                </style>
                <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                    <Nodes>
                       
                    </Nodes>
                </asp:TreeView>
            </div>
        </div>
    </div>
</div>

        <!-- Right Side Content -->
        <div class="col-6 col-md-9" id="sectionDetailsContainer">
            
            <div>
                <asp:Label ID="lblDocDESC" runat="server" CssClass="doc-details doc-lblDocDESC"></asp:Label>
            </div>
            <div>
   
    <asp:Label ID="lblVNo" runat="server" CssClass="doc-details" ></asp:Label>
                </div>
            <div>
    
    <asp:Label ID="lblBNo" runat="server" CssClass="doc-details" ></asp:Label>
</div>
            <div>
                <asp:Label ID="lblHeader" runat="server" CssClass="doc-details doc-header"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblSec1" runat="server" CssClass="doc-details"></asp:Label>
            </div>
            <div>
                <br />
            </div>
            <div>
                <asp:Label ID="lblCon1" runat="server" CssClass="doc-details"></asp:Label>
            </div>
            <div>
                <br />
            </div>
            <div>
                <asp:Label ID="lblSec2" runat="server" CssClass="doc-details"></asp:Label>
            </div>
            <div>
                <br />
            </div>
            <div>
                <asp:Label ID="lblCon2" runat="server" CssClass="doc-details"></asp:Label>
            </div>
            <div>
                <br />
            </div>
            <div>
                <asp:Label ID="lblFooter" runat="server" CssClass="doc-details"></asp:Label>
            </div>
            <style>
               .doc-lblDocDESC {
            font-weight: bold;
            font-size: 35px;
            text-align: left; /* Left-align text */
            line-height: 1.2; /* Adjust line height as needed */
        }
               .detail-label{
                   text-align:left
               }
               .doc-header{
                   font-size:20px;
                   font-weight:bold;
               }

        
            </style>
        </div>
    </div>
</div>
           </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
