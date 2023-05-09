<%@ Page Title="" Language="C#" MasterPageFile="~/SiteApp.master" AutoEventWireup="true" CodeBehind="webfrmPWOMSReport.aspx.cs" Inherits="PWOMS.webfrmPWOMSReport" %>

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
                                <%--<div class="row">
                                    <div class="form-group col-sm-3">
                                        <asp:Label ID="lblCustomer" CssClass="ctrlStyle_Label" runat="server" Text="[2] Customer"></asp:Label>
                                        <asp:DropDownList ID="ddlCustomer" CssClass="form-control form-group-sm" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-sm-3">
                                        <asp:Label ID="lblOrederType" CssClass="ctrlStyle_Label" runat="server" Text="[3] Order Type"></asp:Label>
                                        <asp:DropDownList ID="ddlOrderType" CssClass="form-control form-group-sm" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-sm-3">
                                        <asp:Label ID="lblBrandBuyer" CssClass="ctrlStyle_Label" runat="server" Text="[4] Brand/Buyer"></asp:Label>
                                        <asp:DropDownList ID="ddlBrand_Buyer" CssClass="form-control form-group-sm" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                            </div>

                            <div class="form-row" style="margin-top: 10px;">
                                <div class="form-group col-sm-12">
                                    <div id="DOCX" runat="server">
                                    </div>
                                </div>
                            </div>

                            <div class="container-fluid myFrame">

                                <div class="form-row col-sm-3 myCard text-center" style="margin: 5px;">
                                    <asp:ImageButton ID="imgbtnXL201" runat="server"
                                        CommandName="Employee Info" AlternateText="Employee Info"
                                        OnClick="imgbtnXL201_Click" ImageUrl="~/picture/reports.png" Height="40px"
                                        Width="40px" />
                                    <br />
                                    <asp:Label ID="lblPWOMS201" CssClass="ctrlStyle_Label" runat="server" Text="[PWOMS201] <br /> Employee Info" AssociatedControlID="imgbtnXL201"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblParameterPWOMS201" CssClass="ctrlStyle_Label" runat="server" Text="(note: [1A],[1B](Update On) parameter input needed for this view process)"></asp:Label>
                                </div>

                                <div class="form-row  col-sm-3 myCard text-center" style="margin: 5px;">
                                    <asp:ImageButton ID="imgbtnXL202" runat="server"
                                        CommandName="Bank Info" AlternateText="Bank Info"
                                        OnClick="imgbtnXL202_Click" ImageUrl="~/picture/reports.png" Height="40px"
                                        Width="40px" />
                                    <br />
                                    <asp:Label ID="lblPWOMS202" CssClass="ctrlStyle_Label" runat="server" Text="[PWOMS202]<br /> Bank Info" AssociatedControlID="imgbtnXL202"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblParameterPWOMS202" CssClass="ctrlStyle_Label" runat="server" Text="(note: [1A],[1B](Update On) parameter input needed for this view process)"></asp:Label>
                                </div>

                                <div class="form-row  col-sm-3 myCard text-center" style="margin: 5px;">
                                    <asp:ImageButton ID="imgbtnXL203" runat="server"
                                        CommandName="Lease Agreement Info" AlternateText="Lease Agreement Info"
                                        OnClick="imgbtnXL203_Click" ImageUrl="~/picture/reports.png" Height="40px"
                                        Width="40px" />
                                    <br />
                                    <asp:Label ID="lblPWOMS203" CssClass="ctrlStyle_Label" runat="server" Text="[PWOMS203]<br /> Lease Agreement Info" AssociatedControlID="imgbtnXL203"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblParameterPWOMS203" CssClass="ctrlStyle_Label" runat="server" Text="(note: [1A],[1B](Update On) parameter input needed for this view process)"></asp:Label>
                                </div>

                                <div class="form-row  col-sm-3 myCard text-center" style="margin: 5px;">
                                    <asp:ImageButton ID="imgbtnXL204" runat="server"
                                        CommandName="Funding Info" AlternateText="Funding Info"
                                        OnClick="imgbtnXL204_Click" ImageUrl="~/picture/reports.png" Height="40px"
                                        Width="40px" />
                                    <br />
                                    <asp:Label ID="lblPWOMS204" CssClass="ctrlStyle_Label" runat="server" Text="[PWOMS204]<br /> Funding Info" AssociatedControlID="imgbtnXL204"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblParameterPWOMS204" CssClass="ctrlStyle_Label" runat="server" Text="(note: [1A],[1B](Update On) parameter input needed for this view process)"></asp:Label>
                                </div>

                                <div class="form-row  col-sm-3 myCard text-center" style="margin: 5px;">
                                    <asp:ImageButton ID="imgbtnXL205" runat="server"
                                        CommandName="Business Registration Info" AlternateText="Business Registration Info"
                                        OnClick="imgbtnXL205_Click" ImageUrl="~/picture/reports.png" Height="40px"
                                        Width="40px" />
                                    <br />
                                    <asp:Label ID="lblPWOMS205" CssClass="ctrlStyle_Label" runat="server" Text="[PWOMS205]<br /> Business Registration Info" AssociatedControlID="imgbtnXL205"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblParameterPWOMS205" CssClass="ctrlStyle_Label" runat="server" Text="(note: [1A],[1B](Update On) parameter input needed for this view process)"></asp:Label>
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
            <asp:PostBackTrigger ControlID="imgbtnXL201" />
            <asp:PostBackTrigger ControlID="imgbtnXL202" />
            <asp:PostBackTrigger ControlID="imgbtnXL203" />
            <asp:PostBackTrigger ControlID="imgbtnXL204" />
            <asp:PostBackTrigger ControlID="imgbtnXL205" />

        </Triggers>
    </asp:UpdatePanel>

    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="position: fixed; z-index: 999; min-height: 100%; width: 100%; top: 0px; background-color: whitesmoke; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8;">
                <div style="position: absolute; z-index: 1200; top: 50%; left: 50%; width: 15%; background-color: whitesmoke; transform: translate(-50%, -50%);">
                    <p>Please wait.... </p>
                    <img alt="" src="Picture/loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>

