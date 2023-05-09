<%@ Page Title="" Language="C#" MasterPageFile="~/SiteApp.master" AutoEventWireup="true" CodeBehind="AppFixedEntityVarManage.aspx.cs" Inherits="PWOMS.AppFixedEntityVarManage" %>

<%@ MasterType VirtualPath="~/SiteApp.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                <div class="col-sm-12 text-right">
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
                                <div class="form-group col-sm-3">
                                    <br />
                                    <asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" Text="View" OnClick="btnSearch_Click" />
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
                                        <asp:DataGrid ID="dgSearch" runat="server" CssClass="datagrid" Font-Size="10pt" Font-Names="Calibri,Tahoma,Verdana,Arial"
                                            CellPadding="4" GridLines="None" ForeColor="#333333">
                                            <AlternatingItemStyle BorderWidth="1px" BorderStyle="Groove" BorderColor="White"
                                                BackColor="White" ForeColor="#284775"></AlternatingItemStyle>
                                            <EditItemStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BorderStyle="None" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:ButtonColumn Text="[Select]" CommandName="EditFile" ItemStyle-Font-Underline="False"
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
                    <%--Msg Board--%>
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
                                    <asp:Button ID="dlgCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="dlgCancel_Click" />
                                    <br />
                                    <asp:Label ID="lblDlgState" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <%--Msg Board End--%>
                    <%--Data Entry--%>
                    <asp:View ID="vw02" runat="server">
                        <div class="container-fluid roundCornerImg" style="margin-top: 10px; padding-top: 5px; padding-bottom: 5px; background-color: white;">
                            <div class="row">
                                <div class="col-sm-3 btn-group" role="group">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3" style="text-align: right;">
                                    <asp:Button ID="btnLogOff" CssClass="btn btn-default" runat="server" Text="Close" OnClick="btnLogOff_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-3 required">
                                    <asp:Label ID="lblEntityTypeList" CssClass="ctrlStyle_Label" runat="server" Text="Entity type"></asp:Label>
                                    <asp:DropDownList ID="ddlEntityType" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-3">
                                    <br />
                                    <asp:Button ID="Button_Select" CssClass="btn btn-default" runat="server" Text="Select" OnClick="Button_Select_Click" />
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="form-group col-sm-3" style="text-align: right">
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-3">
                                    <asp:Button ID="btnAddNew" CssClass="btn btn-default" runat="server" Text="Add New" OnClick="btnAddNew_Click" />&nbsp;
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-sm-3 required">
                                    <asp:Label ID="lblEntityTpe" CssClass="ctrlStyle_Label" runat="server" Text="Entity type"></asp:Label>
                                    <asp:TextBox ID="txtEntryType" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3 required">
                                    <asp:Label ID="lblCode" CssClass="ctrlStyle_Label" runat="server" Text="Code" required="required"></asp:Label>
                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblDescription" CssClass="ctrlStyle_Label" runat="server" Text="Description"></asp:Label>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3">
                                    <asp:Label ID="lblEntityValue" CssClass="ctrlStyle_Label" runat="server" Text="Value"></asp:Label>
                                    <asp:TextBox ID="txtValue" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6 red-forecolor text-left">
                                    All the red (*) marked fields are mandatory
                                </div>
                                <div class="col-sm-6" style="text-align: right;">
                                    <div class="btn-group" role="group" aria-label="Basic example" style="padding-right: 10px;">
                                        <asp:Button ID="Button_Save" type="submit" CssClass="btn btn-default" runat="server" Text="Save" OnClick="Button_Save_Click" />
                                        <asp:Button ID="Button_Delete" CssClass="btn btn-default" runat="server" Text="Delete" OnClick="Button_Delete_Click" />
                                        <asp:Button ID="Button_Cancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="Button_Cancel_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <div class="row" style="padding-left: 10px; padding-right: 10px;">
                                <asp:Panel ID="PanelGridViewFixedVariable" runat="server" Height="160px" ScrollBars="Auto">
                                    <asp:GridView ID="GridViewFixedVariable" runat="server" CssClass="table table-striped table-bordered table-condensed" AutoGenerateColumns="False" CellPadding="4"
                                        DataKeyNames="EntityType" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#333333"
                                        GridLines="Both" OnRowCommand="GridViewFixedVariable_Command">
                                        <RowStyle BackColor="White" />
                                        <Columns>

                                            <asp:BoundField AccessibleHeaderText="EntityType" DataField="EntityType" HeaderText="EntityType"
                                                SortExpression="EntityType" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:BoundField AccessibleHeaderText="Code" DataField="Code" HeaderText="Code"
                                                SortExpression="Code" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description"
                                                SortExpression="Description" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:BoundField AccessibleHeaderText="Value" DataField="Value" HeaderText="Value"
                                                SortExpression="Value" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:ButtonField runat="server" ButtonType="Link" CommandName="SELECT" ControlStyle-ForeColor="Blue"
                                                Text="SELECT" HeaderText="SELECT" SortExpression="SELECT" ItemStyle-Width="10px"
                                                ControlStyle-Font-Size="9px"></asp:ButtonField>


                                        </Columns>
                                        <FooterStyle BackColor="#70151B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#70151B" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#FF0D75B6" Font-Bold="True" ForeColor="White"
                                            HorizontalAlign="Left" />
                                        <EditRowStyle BackColor="#FFFFFF" ForeColor="Black" />
                                        <AlternatingRowStyle BackColor="#ededed" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="TxtMsgBox" CssClass="form-control form-group-sm ctrlStyle_FullLength red-forecolor" runat="server" TextMode="MultiLine">Common Test</asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>

            </div>

        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnCancelSearch" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="dlgOk" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="dlgCancel" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="Button_Select" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnAddNew" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="Button_Save" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="Button_Delete" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="Button_Cancel" />


            <%--if there are any file uploader; mention it in Post Back trigger; not in AsyncPostbackTrigger--%>
            <asp:PostBackTrigger ControlID="btnLogOff" />

        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="position: fixed; z-index: 999; min-height: 100%; width: 100%; top: -1px; left: -1px; background-color: whitesmoke; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8;">
                <div style="position: absolute; z-index: 1200; top: 50%; left: 50%; width: 15%; background-color: whitesmoke; transform: translate(-50%, -50%);">
                    <p>Please wait.... </p>
                    <img alt="" src="Picture/loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
