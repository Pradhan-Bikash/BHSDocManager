<%@ Page Language="C#" EnableEventValidation="false" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/SiteApp.master" CodeBehind="WebFrmDocMgtView.aspx.cs" Inherits="BPWEBAccessControl.DocMgtView" %>

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
                <asp:MultiView ID="mvwDataVw" ActiveViewIndex="2" runat="server">
                    <%--Search Start--%>
                    <asp:View ID="vw00" runat="server">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-12 text-left">
                                    <asp:Label ID="lblSearchTitle" CssClass="ctrlStyle_Label font-size-xxl ashblue-forecolor" runat="server">Search</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblSearchBy" CssClass="ctrlStyle_Label" runat="server" Text="Search By"></asp:Label>
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblValue" CssClass="ctrlStyle_Label" runat="server" Text="Value"></asp:Label>
                                    <asp:TextBox ID="tbValue" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3" style="margin-top: 20px">

                                    <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" Text="Serach" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnCancelSearch" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancelSearch_Click" />
                                </div>
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblViewState" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblViewName" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblInfoDox" CssClass="ctrlStyle_Label normal bold" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Panel ID="panSearch" runat="server" Height="400px" CssClass="table-responsive" ScrollBars="Auto">
                                        <asp:DataGrid ID="dgSearch" runat="server" CssClass="datagrid table table-striped table-bordered table-condensed" Font-Size="10pt" Font-Names="Calibri,Tahoma,Verdana,Arial" OnItemCommand="Grid_Command"
                                            CellPadding="4" GridLines="None" ForeColor="#333333">
                                            <AlternatingItemStyle BorderWidth="1px" BorderStyle="Groove" BorderColor="White"
                                                BackColor="White" ForeColor="#284775"></AlternatingItemStyle>
                                            <EditItemStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BorderStyle="None" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:ButtonColumn Text="[View]" CommandName="ViewDoc" ItemStyle-Font-Underline="False"
                                                    ItemStyle-ForeColor="blue" ButtonType="LinkButton">
                                                    <ItemStyle Font-Underline="False" ForeColor="Blue" />
                                                </asp:ButtonColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <%--Search End--%>
                    <%--Show Message Dialog--%>
                    <asp:View ID="vw01" runat="server">
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
                    <asp:View ID="vw02" runat="server">
                        <!-- Left Sidebar -->
                        <!-- Left Sidebar -->
                        <div class="col-sm-6 col-md-3">
                            <div class="sidebar_wrapper">
                                <div class="sideBar sticky">
                                    <div class="sideBar--wrap newLeftbar">
                                        <div class="row" style="margin:5px">
                                            <div class="col-md-4">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default btn-block btnSearch" Text="Search" OnClick="btnSearchView_Click" />
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
                                <asp:Label ID="lblVNo"  runat="server" CssClass="doc-details"></asp:Label>
                            </div>
                            <div></div>

                            <div>
                                <span><b>Build No: </b></span>
                                <asp:Label ID="lblBNo"  runat="server" CssClass="doc-details"></asp:Label>
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
                                    <asp:Button ID="btnDownload1" Visible="false" runat="server" Text="Download File 1" OnClick="btnDownload1_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnDownload2" Visible="false" runat="server" Text="Download File 2" OnClick="btnDownload2_Click" CssClass="btn btn-primary ml-2" />
                                    <asp:Button ID="btnDownload3" Visible="false" runat="server" Text="Download File 3" OnClick="btnDownload3_Click" CssClass="btn btn-primary ml-2" />
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
</asp:Content>
