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
               <div class="row g-0 text-center">
                   <div class="col-sm-6 col-md-4">
                       <div class="sidebar_wrapper">
                           <div class="sideBar sticky ">
                               <div class="sideBar--wrap newLeftbar  ">
                                   <ul class="leftBarList" style="list-style-type:none">
                                       <div id="sidebarContent" runat="server" class="first"></div>
                                   </ul>
                               </div>
                           </div>
                       </div>
                   </div>
                   <div class="col-6 col-md-8" id="sectionDetailsContainer">
                       <!-- Placeholder for section details -->
                   </div>
               </div>
           </div>
            <script type="text/javascript">
                function toggleSection(contentId) {
                    var section = document.getElementById(contentId);
                    if (section.style.display === "none") {
                        section.style.display = "block";
                    } else {
                        section.style.display = "none";
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
