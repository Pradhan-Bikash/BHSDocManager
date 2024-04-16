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
                <%--<div class="container-fluid">--%>
                    <div class="row g-0">
                        <!-- Left Sidebar -->
                        <div class="col-sm-6 col-md-3">
                            <div class="sidebar_wrapper">
                                <div class="sideBar sticky">
                                    <div class="sideBar--wrap newLeftbar">
                                        <asp:TreeView ID="TreeView1" runat="server" RootNodeStyle-CssClass="Root-NodeStyle" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" >
                                            <Nodes>
                                            </Nodes>
                                        </asp:TreeView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Right Side Content -->
                        <div class="col-6 col-md-9" id="sectionDetailsContainer";>

                            <div>
                                <asp:Label ID="lblDocDESC" runat="server" CssClass="doc-details doc-lblDocDESC"></asp:Label>
                            </div>
                            <div>
                                <span><b>Version No: </b></span>
                                <asp:Label ID="lblVNo" runat="server" CssClass="doc-details"></asp:Label>
                            </div>
                            <div></div>
                         
                            <div>
                                <span><b>Build No: </b></span>
                                <asp:Label ID="lblBNo" runat="server" CssClass="doc-details"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <div>
                                <asp:Label ID="lblHeader" runat="server" CssClass="doc-details doc-header"></asp:Label>
                            </div>
                            <div id="dvSec1" runat="server">
                             </div>
                            
                                <br />
             
                            <div id="dvCon1" runat="server" class="Div-Style-Content">
                            </div>
                        
                                <br />
                            <br />
                            <div id="dvSec2" runat="server">
                            </div>
                            <br />
                                <br />
                           
                            <div id="dvCon2" runat="server">
                            </div>
                         <br />
                                <br />
                           
                            <div>
                                <asp:Label ID="lblFooter" runat="server" CssClass="doc-details"></asp:Label>
                            </div>
                            <div>
                                <!-- Download Buttons -->
                                <div class="text-center mt-4">
                                    <asp:Button ID="btnDownload1" runat="server" Text="Download File 1" OnClick="btnDownload1_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnDownload2" runat="server" Text="Download File 2" OnClick="btnDownload2_Click" CssClass="btn btn-primary ml-2" />
                                    <asp:Button ID="btnDownload3" runat="server" Text="Download File 3" OnClick="btnDownload3_Click" CssClass="btn btn-primary ml-2" />
                                </div>
                            </div>
                           
                        </div>
                 
                <%--</div>--%>
            </div>
                        
            </div>
            
        </ContentTemplate>
        <Triggers>
             <asp:PostBackTrigger ControlID="btnDownload1" />
            <asp:PostBackTrigger ControlID="btnDownload2" />
            <asp:PostBackTrigger ControlID="btnDownload3" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
