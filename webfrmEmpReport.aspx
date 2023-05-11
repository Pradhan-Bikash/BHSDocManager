<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteApp.master" CodeBehind="webfrmEmpReport.aspx.cs" Inherits="BPWEBAccessControl.webfrmEmpReport" %>

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
                                        <asp:DataGrid ID="dgSearch" runat="server" CssClass="datagrid" Font-Size="10pt" Font-Names="Calibri,Tahoma,Verdana,Arial" OnItemCommand="Grid_Command"
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
                        <div class="container-fluid">
                            <div class="col-sm-6" style="margin-top: 40px; background-color: #cce6ff;">
                                <asp:Image ID="dlgImage" runat="server" CssClass="img-responsive" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="Picture/info.png" />
                                <br />
                                <asp:Label ID="lblMessageBoard" runat="server" CssClass="ctrlStyle_Label grey-forecolor font-size-xxl">Message Board</asp:Label>
                                <br />
                                <asp:Label ID="dlgMsg" CssClass="card-text" runat="server"></asp:Label>
                                <br />
                                <br />
                                <asp:Button ID="dlgOk" CssClass="btn btn-default" runat="server" Text="OK" OnClick="dlgOk_Click" />
                                <asp:Button ID="dlgCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="dlgCancel_Click" />
                                <br />
                                <asp:Label ID="lblDlgState" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="margin-top: 40px;"></div>
                        </div>
                    </asp:View>
                    <%--Msg Board End--%>
                    <%--Data Entry--%>
                    <asp:View ID="vw02" runat="server">
                        <div class="container-fluid roundCornerImg" style="margin-top: 10px; padding-top: 5px; padding-bottom: 5px; background-color: white;">

                            <div class="row">
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3" style="text-align: right;">
                                    <asp:Button ID="btnLogOff" CssClass="btn btn-default" runat="server" Text="Close" OnClick="btnLogOff_Click" />
                                </div>
                            </div>

                            <div class="container-fluid myFrame">
                                <div class="row">
                                    <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblFromDate" CssClass="ctrlStyle_Label" runat="server" Text="[1A] From Date (dd-MMM-yyyy)" required="required"></asp:Label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy" CssClass="ajaxcald"></ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblToDate" CssClass="ctrlStyle_Label" runat="server" Text="[1B] To Date (dd-MMM-yyyy)" required="required"></asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy" CssClass="ajaxcald"></ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>

                               <div class="row">
                                    <div class="form-group col-sm-3 required">
                                        <asp:Label ID="lblReportGrp" ForeColor="Blue" CssClass="ctrlStyle_Label" runat="server" Text="Report Group" required="required"></asp:Label>
                                         <asp:DropDownList ID="ddlReportGrp" CssClass="form-control form-group-sm" runat="server"></asp:DropDownList>
                                    </div>  
                                   <%--<div class="form-row" style="margin-top: 10px;">--%>
                                    <div class="form-group col-sm-6" style="margin-top: 16px;">
                                        <div id="DOCX" runat="server">
                                            <asp:Label ID="Label1" ForeColor="Blue" CssClass="ctrlStyle_Label" runat="server" Text="Text Text  Text  Text  Text  Text  Text  Text Text Text  Text  Text  Text  Text  Text  Text"></asp:Label>
                                         </div>
                                    </div>
                                   <%--</div>--%>
                                </div>
                             </div>

                            <%--<div class="form-row" style="margin-top: 10px;">
                                <div class="form-group col-sm-12">
                                    <div id="DOCX" runat="server">
                                        <asp:Label ID="Label1" CssClass="ctrlStyle_Label" runat="server" Text="Text"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="container-fluid myFrame">

                              <div class="row">
                                <div class="col-sm-12">
                                    <asp:Panel ID="Panel1" runat="server" Height="400px" CssClass="table-responsive" ScrollBars="Auto">
                                        <asp:DataGrid ID="DataGrid1" OnItemDataBound='ItemDB' runat="server" CssClass="datagrid table table-striped table-bordered table-condensed" Font-Size="10pt" Font-Names="Calibri,Tahoma,Verdana,Arial" OnItemCommand="Grid_Command"
                                            CellPadding="4" GridLines="None" ForeColor="#333333">
                                            <AlternatingItemStyle BorderWidth="1px" BorderStyle="Groove" BorderColor="White"
                                                BackColor="White" ForeColor="#284775"></AlternatingItemStyle>
                                            <EditItemStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BorderStyle="None" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <%--<asp:BoundField HeaderText="Column 1" DataField="Column1" ControlStyle-CssClass="Group 1" />
                                                <asp:BoundField HeaderText="Column 1" DataField="Column1" ControlStyle-CssClass="Group 1" />
                                                <asp:BoundField HeaderText="Column 1" DataField="Column1" ControlStyle-CssClass="Group 1" />--%>

                                                <asp:ButtonColumn HeaderImageUrl="~/picture/reports.png" CommandName="EditFile" ItemStyle-Font-Underline="False"
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
                                <div class="form-row col-sm-3 myCard text-center" style="margin: 5px;">
                                    <asp:ImageButton ID="imgbtnXL206" runat="server"
                                        CommandName="Employee" AlternateText="Employee"
                                        OnClick="imgbtnXL206_Click" ImageUrl="~/picture/reports.png" Height="40px"
                                        Width="40px" />
                                    <br />
                                    <asp:Label ID="lblPWOMS206" CssClass="ctrlStyle_Label" runat="server" Text="[PWOMS206] <br /> Employee" AssociatedControlID="imgbtnXL206"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblParameterPWOMS206" CssClass="ctrlStyle_Label" runat="server" Text="(note: [1A],[1B](Date Of Join) parameter input needed for this view process)"></asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <asp:TextBox ID="TxtMsgBox" CssClass="form-control form-group-sm ctrlStyle_FullLength red-forecolor" runat="server" TextMode="MultiLine">Common Test</asp:TextBox>
                            </div>
                        </div>


                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger EventName="click" ControlID="dlgCancel" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="dlgOk" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnCancelSearch" />
            <%--<asp:AsyncPostBackTrigger EventName="click" ControlID="ibTeleContactsChart" />--%>

            <%--if there are any file uploader; mention it in Post Back trigger; not in AsyncPostbackTrigger--%>
            <asp:PostBackTrigger ControlID="btnLogOff" />
            <asp:PostBackTrigger ControlID="imgbtnXL206" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

