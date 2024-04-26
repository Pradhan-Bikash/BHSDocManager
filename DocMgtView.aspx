<%@ Page Language="C#" EnableEventValidation="false" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/SiteApp.master" CodeBehind="DocMgtView.aspx.cs" Inherits="BPWEBAccessControl.DocMgtView" %>

<%@ MasterType VirtualPath="~/SiteApp.master" %>
<asp:Content ID="Content1" ValidateRequest="false" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid frmbaseContainer02" id="dataUpdatePanel" runat="server">
                <div class="row frmbaseContainer02-header">
                    <div class="col-sm-12 text-right">
                        <asp:Label ID="lblfrmName" runat="server">frmName</asp:Label>
                    </div>
                </div>
                <asp:MultiView ID="mvwDataVw" ActiveViewIndex="1" runat="server">

                    <%--Show Message Dialog--%>
                    <asp:View ID="vw00" runat="server">
                        <div class="container-fluid myMsgBoxStyleContainer">
                            <div class="myMsgBoxStyle">
                                <div class="myMsgBoxStyle-header">
                                    <table style="border: none;">
                                        <tr>
                                            <td style="width: 30%;">
                                                <asp:Image ID="dlgImage" runat="server" CssClass="img-responsive" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="Picture/info.png" />
                                            </td>
                                            <td style="width: 70%; padding-left: 10px;">
                                                <asp:Label ID="lblMessageBoard" runat="server" CssClass="ctrlStyle_Label grey-forecolor font-size-xxl">Message</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="myMsgBoxStyle-body">
                                    <br />
                                    <asp:Label ID="dlgMsg" CssClass="card-text" runat="server" Style="word-wrap: break-word;"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Button ID="dlgOk" CssClass="btn btn-default" runat="server" Text="OK" OnClick="dlgOk_Click" />

                                    <br />
                                    <asp:Label ID="lblDlgState" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--Content Load--%>
                    <asp:View ID="vw01" runat="server">
                        <!-- Left Sidebar -->
                        <!-- Left Sidebar -->
                        <div class="col-sm-6 col-md-3">
                            <div class="sidebar_wrapper">
                                <div class="sideBar sticky">
                                    <div class="sideBar--wrap newLeftbar">
                                        <div class="row" style="margin:5px">
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtSearch" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Search Something....."></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default btn-block btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>

                                        <asp:TreeView ID="TreeView1" runat="server" RootNodeStyle-CssClass="Root-NodeStyle" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <!-- Right Side Content -->
                        <div class="col-6 col-md-9" runat="server" id="sectionDetailsContainer" style="display: none">

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
                            <div id="dvCon1" runat="server">
                            </div>
                            <br />
                            <div id="dvSec2" runat="server">
                            </div>
                            <br />
                            <div id="dvCon2" runat="server">
                            </div>
                            <br />
                            <br />

                            <div class="text-center">
                                <asp:Label ID="lblFooter" runat="server" CssClass="doc-details"></asp:Label>
                            </div>
                            <br />

                            <div>
                                <!-- Download Buttons -->
                                <div class="text-center mt-4">
                                    <asp:Button ID="btnDownload1" runat="server" Text="Download File 1" OnClick="btnDownload1_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnDownload2" runat="server" Text="Download File 2" OnClick="btnDownload2_Click" CssClass="btn btn-primary ml-2" />
                                    <asp:Button ID="btnDownload3" runat="server" Text="Download File 3" OnClick="btnDownload3_Click" CssClass="btn btn-primary ml-2" />
                                </div>
                            </div>

                        </div>
                    </asp:View>

                </asp:MultiView>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownload1" />
            <asp:PostBackTrigger ControlID="btnDownload2" />
            <asp:PostBackTrigger ControlID="btnDownload3" />

        </Triggers>
    </asp:UpdatePanel>


    <link href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript">  
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $("#<%= txtSearch.ClientID %>").autocomplete({
                delay: 1,
            source: function (request, response) {
                var searchTerm = $("#<%= txtSearch.ClientID %>").val();
                $.ajax({
                    type: "POST",
                    url: "DocMgtView.aspx/GetSearchResults",
                    data: JSON.stringify({ searchTerm: searchTerm }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert("No Match");
                    }
                });
            }
        });
        }
    </script>--%>


   <script type="text/javascript">  
       $(document).ready(function () {
           initializeAutocomplete();
       });

       function initializeAutocomplete() {
           var searchTextBoxClientId = '<%= txtSearch.ClientID %>';
           $("#" + searchTextBoxClientId).autocomplete({
               delay: 1,
               source: function (request, response) {
                   var searchTerm = $("#" + searchTextBoxClientId).val();
                   $.ajax({
                       type: "POST",
                       url: "DocMgtView.aspx/GetSearchResults",
                       data: JSON.stringify({ searchTerm: searchTerm }),
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (data) {
                           if (data.d.length === 0) {
                               response([{ label: "No matching Document...", value: "", disabled: true }]);
                           } else {
                               response(data.d);
                           }
                       },
                       error: function (result) {
                           alert("Error fetching search results.");
                       }
                   });
               },
               select: function (event, ui) {

                   if (ui.item.disabled) {
                       event.preventDefault();
                       return false;
                   }
               }
           });
       }
   </script>

</asp:Content>
