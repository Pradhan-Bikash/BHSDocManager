<%@ Page Title="" Language="C#" MasterPageFile="~/SiteApp.Master" AutoEventWireup="true" CodeBehind="WebFrmSecurityControl.aspx.cs" Inherits="BPWEBAccessControl.WebFrmSecurityControl" %>

<%@ MasterType VirtualPath="~/SiteApp.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnAddNewUser" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnEditUser" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSaveUser" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnDelUser" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnCancelUser" />

            <asp:AsyncPostBackTrigger EventName="click" ControlID="linbtnRefreshCopyAccess" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="linbtnCancelCopyAcc" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnCopyAcc" />

            <asp:AsyncPostBackTrigger EventName="click" ControlID="linbtnLoad" />
            <asp:AsyncPostBackTrigger EventName="TextChanged" ControlID="tbSysUserId" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="linbtnSysSave" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="linbtnSysDel" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="linbtnSysCancel" />


            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnMenuListUpdate" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnRefresh" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSearchIn" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnReset" />
            <asp:AsyncPostBackTrigger EventName="CheckedChanged" ControlID="cbModule" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSave" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnDelete" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnCancel" />

            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSiteCheck" />
            <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlSiteUsers" />
            <asp:AsyncPostBackTrigger EventName="CheckedChanged" ControlID="cbSelectSites" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSaveSite" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSiteCancel" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnDelSite" />

            <asp:AsyncPostBackTrigger EventName="click" ControlID="dlgOk" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="dlgCancel" />

            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnSearchDate" />
            <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="gvSearch" />
            <asp:AsyncPostBackTrigger EventName="click" ControlID="btnCancelSearch" />
            <%--if there are any file uploader; mention it in Post Back trigger; not in AsyncPostbackTrigger--%>
            <asp:PostBackTrigger ControlID="btnLogOff" />
            <asp:PostBackTrigger ControlID="tabUserCreate" />
            <asp:PostBackTrigger ControlID="tabUserAcc" />
            <asp:PostBackTrigger ControlID="tabAccCopy" />
            <asp:PostBackTrigger ControlID="tabSysInfo" />
            <asp:PostBackTrigger ControlID="tabSite" />
            <%--<asp:PostBackTrigger ControlID="btnSave" />--%>
            <%--<asp:PostBackTrigger ControlID="linEditAttach1" />
            <asp:PostBackTrigger ControlID="linAttachDel1" />--%>
        </Triggers>
        <ContentTemplate>
            <div class="container-fluid frmbaseContainer02" id="dataUpdatePanel" runat="server">
                <div class="row">
                    <div class="col-sm-9 col-md-9 col-lg-9">
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <asp:LinkButton ID="tabUserCreate" runat="server" Text="App User Info" OnClick="tab_Click" CommandName="UserCreate"></asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="tabUserAcc" runat="server" Text="App Access Control" OnClick="tab_Click" CommandName="AppAccess"></asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="tabAccCopy" runat="server" Text="Access Copy Utility" OnClick="tab_Click" CommandName="AccessCopy"></asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="tabSysInfo" runat="server" Text="System Admin Info" OnClick="tab_Click" CommandName="SysInfo"></asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="tabSite" runat="server" Text="Site Access" OnClick="tab_Click" CommandName="SiteAccess"></asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                    <div class="col-sm-3 col-md-3 col-lg-3" style="text-align: right;">
                        <asp:LinkButton ID="btnLogOff" TabIndex="0"
                            runat="server" OnClick="btnLogOff_Click"
                            CssClass="btn btn-danger">
                                <span aria-hidden="true" class="glyphicon glyphicon-remove-circle"></span>&nbsp;Close
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-sm-12 col-md-12 col-lg-12 w3-white w3-round-medium">
                        <asp:MultiView ID="mvwDataVw" ActiveViewIndex="2" runat="server">
                            <%--Search Start--%>
                            <asp:View ID="vw00" runat="server" OnActivate="vwActivate_DisableLogOff">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12 text-right">
                                            <asp:Label ID="lblSearchTitle" ForeColor="#2b5797" Font-Bold="true" runat="server">Search</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <span for="tbFromDate">From Date</span>
                                            <asp:TextBox ID="tbFromDate" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="tbFromDate" Format="dd-MMM-yyyy" CssClass="ajaxcald"></ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <span for="tbToDate">To Date</span>
                                            <asp:TextBox ID="tbToDate" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="tbToDate" Format="dd-MMM-yyyy" CssClass="ajaxcald"></ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <br />
                                            <asp:LinkButton ID="btnSearchDate"
                                                runat="server" OnClick="btnSearchDate_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>&nbsp;View
                                            </asp:LinkButton>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <asp:Label ID="lblSearchBy" runat="server" Text="Search By"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <asp:Label ID="lblValue" runat="server" Text="Value"></asp:Label>
                                            <asp:TextBox ID="tbValue" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <br />
                                            <asp:LinkButton ID="btnSearch"
                                                runat="server" OnClick="btnSearch_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>&nbsp;View
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelSearch"
                                                runat="server" OnClick="btnCancelSearch_Click"
                                                CssClass="btn btn-info">
                                <span aria-hidden="true" class="glyphicon glyphicon-ban-circle"></span>&nbsp;Cancel
                                            </asp:LinkButton>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <asp:Label ID="lblViewState" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblViewName" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">
                                            <asp:Label ID="lblSearchResult" ForeColor="#ee1111" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Panel ID="panSearch" runat="server" Width="100%" Height="280px" BorderStyle="None"
                                                ScrollBars="Auto" BackColor="#ECECEC">
                                                <asp:GridView ID="gvSearch" runat="server" Font-Size="8pt" CssClass="table table-striped table-bordered table-condensed" AutoGenerateColumns="true" AutoGenerateSelectButton="true" OnSelectedIndexChanged="gvSearch_SelectedIndexChanged" Font-Names="Tahoma" Width="100%" CellPadding="4" BorderWidth="1px" DataKeyNames="UserID" ForeColor="#333333" UseAccessibleHeader="true"
                                                    GridLines="Both">
                                                    <RowStyle BackColor="#FFFFFF" />
                                                    <FooterStyle BackColor="#070151B" ForeColor="White" Font-Bold="True" />
                                                    <PagerStyle BackColor="#070151B" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#FFFFCB" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#263E59" Font-Bold="True" ForeColor="#FFFFFF" HorizontalAlign="Left" />
                                                    <EditRowStyle BackColor="#FFFFFF" ForeColor="Black" />
                                                    <AlternatingRowStyle BackColor="#EBEBEB" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <%--Search End--%>
                            <%--Msg Board--%>
                            <asp:View ID="vw01" runat="server" OnActivate="vwActivate_DisableLogOff">
                                <div class="container-fluid">
                                    <div class="col-sm-6 myCard" style="margin-top: 40px; background-color: #cce6ff;">
                                        <asp:Image ID="dlgImage" runat="server" CssClass="img-responsive" AlternateText="" ImageAlign="AbsMiddle" ImageUrl="Depiction/info.png" />
                                        <br />
                                        <asp:Label ID="lblMessageBoard" runat="server" CssClass="grey-forecolor font-size-xxl">Message Board</asp:Label>
                                        <br />
                                        <asp:Label ID="dlgMsg" CssClass="card-text" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:LinkButton ID="dlgOk"
                                            runat="server" OnClick="dlgOk_Click"
                                            CssClass="btn btn-success">
                                <span aria-hidden="true" class="glyphicon glyphicon-thumbs-up"></span>&nbsp;Ok
                                        </asp:LinkButton>
                                        &nbsp;
                            <asp:LinkButton ID="dlgCancel"
                                runat="server" OnClick="dlgCancel_Click"
                                CssClass="btn btn-info">
                                <span aria-hidden="true" class="glyphicon glyphicon-alert"></span>&nbsp;Cancel
                            </asp:LinkButton>
                                        <br />
                                        <asp:Label ID="lblDlgState" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-sm-6" style="margin-top: 40px;"></div>
                                </div>
                            </asp:View>
                            <%--Msg Board End--%>
                            <%--App Access Control--%>
                            <asp:View ID="vw02" runat="server" OnActivate="vwActivate_EnableLogOff">
                                <div class="container-fluid" style="margin-top: 10px;">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <asp:LinkButton ID="btnMenuListUpdate"
                                                runat="server" OnClick="btnMenuListUpdate_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>&nbsp;Check and Update
                                            </asp:LinkButton>&nbsp;                            
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <span for="ddlUserID">User ID</span>
                                            <asp:DropDownList ID="ddlUserID" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3" style="text-align: left;">
                                            <br />
                                            <asp:LinkButton ID="btnRefresh"
                                                runat="server" onmouseup="showPleaseWait()" OnClick="btnRefresh_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp;Refresh
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <h2><small>App Access Control</small></h2>
                                        </div>
                                    </div>
                                    <hr class="blue-backcolor" />
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12">
                                            <asp:Label ID="lblAppAccessInSearch" runat="server" Text="Search" ForeColor="#3366ff"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3 bg-secondary">
                                            <span for="ddlSearchKey">Search By</span>
                                            <asp:DropDownList ID="ddlSearchKey" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3 bg-secondary">
                                            <span for="tbSearchValue">Value</span>
                                            <asp:TextBox ID="tbSearchValue" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3 bg-secondary"></div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3 bg-secondary">
                                            <br />
                                            <asp:LinkButton ID="btnSearchIn" runat="server" OnClick="btnSearchIn_Click" CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp;Search
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnReset" runat="server" OnClick="btnMenuListUpdate_Click" CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp;Reset
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <hr class="blue-backcolor" />
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <asp:CheckBox ID="cbModule" runat="server" Text="Select / De-Select Modules" OnCheckedChanged="cbModule_CheckedChanged" AutoPostBack="true" class="form-check-input" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
                                            <asp:Panel ID="panGrid" runat="server" Width="100%" Height="300px" BorderStyle="None"
                                                ScrollBars="Auto">
                                                <asp:GridView ID="MyDataGrid" runat="server" Width="100%" Font-Size="8pt" AutoGenerateColumns="False"
                                                    Font-Names="Tahoma" CellPadding="4" OnRowDataBound="RowDataBound_Status" BorderWidth="1px"
                                                    DataKeyNames="PRIKEY" ForeColor="#333333" GridLines="Horizontal">
                                                    <RowStyle BackColor="#FFFFFF" />
                                                    <Columns>
                                                        <asp:BoundField AccessibleHeaderText="RATE" DataField="RATE" HeaderText="RATE" ItemStyle-Width="30px"
                                                            Visible="false" />
                                                        <asp:BoundField AccessibleHeaderText="ModuleID" DataField="ModuleID" HeaderText="Module ID"
                                                            SortExpression="ModuleID" ItemStyle-Width="50px" />
                                                        <asp:BoundField AccessibleHeaderText="ModuleName" DataField="MODULENAME" HeaderText="Module Name"
                                                            SortExpression="MODULENAME" ItemStyle-Width="700px" />
                                                        <%--<asp:ImageField DataImageUrlFormatString="/Picture/ImageField.png" DataImageUrlField="ImageField.png" ItemStyle-Width="20px" ItemStyle-Height="20px">
                                                        </asp:ImageField>--%>
                                                        <%-- <asp:CheckBoxField AccessibleHeaderText="Access" DataField="ACCESS" HeaderText="Access" ItemStyle-Width="30px" ReadOnly="false"  />
                                                        <asp:CheckBoxField AccessibleHeaderText="Edit" DataField="EDIT" HeaderText="Edit" ItemStyle-Width="30px" ReadOnly="false"/>
                                                        <asp:CheckBoxField AccessibleHeaderText="Delete" DataField="DELETE" HeaderText="Delete" ItemStyle-Width="30px" ReadOnly="false"/>--%>
                                                        <asp:TemplateField HeaderText="Access" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbAccStat" runat="server" Width="40px" BorderStyle="None" Checked='<%# Bind("[ACCESS]") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbEditStat" runat="server" Width="40px" BorderStyle="None" Checked='<%# Bind("[EDIT]") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbDelStat" runat="server" Width="40px" BorderStyle="None" Checked='<%# Bind("[DELETE]") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#070151B" ForeColor="White" Font-Bold="True" />
                                                    <PagerStyle BackColor="#070151B" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#FFFFCB" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#004300" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <EditRowStyle BackColor="#FFFFFF" ForeColor="Black" />
                                                    <AlternatingRowStyle BackColor="#E3FFDB" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12 bg-warning">
                                            <p class="text-info">This is System access administration option. you have to create the user ID at the previous step (tab) and then using this option add the same user ID in system user list and assign the module wise access to operate. Beware about the spelling of user id when you are creating it here. </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <hr class="blue-backcolor" />
                                    <div class="row">
                                        <div class="col-sm-6 red weight-bold">
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:LinkButton ID="btnSave"
                                                runat="server" OnClick="btnSave_Click" onmouseup="showPleaseWait()"
                                                CssClass="btn btn-success">
                                <span aria-hidden="true" class="glyphicon glyphicon-floppy-saved"></span>&nbsp;Save
                                            </asp:LinkButton>
                                            &nbsp;
                            <asp:LinkButton ID="btnDelete"
                                runat="server" OnClick="btnDelete_Click"
                                CssClass="btn btn-warning">
                                <span aria-hidden="true" class="glyphicon glyphicon glyphicon-trash"></span>&nbsp;Delete
                            </asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="btnCancel"
                                                runat="server" OnClick="btnCancel_Click"
                                                CssClass="btn btn-info">
                                <span aria-hidden="true" class="glyphicon glyphicon-alert"></span>&nbsp;Clear All
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <%--App Access Control End--%>
                            <%--App User Info--%>
                            <asp:View ID="vw03" runat="server" OnActivate="vwActivate_EnableLogOff">
                                <div class="container-fluid" style="margin-top: 10px;">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <asp:LinkButton ID="btnAddNewUser"
                                                runat="server" OnClick="btnAddNewUser_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-user"></span>&nbsp;Add New User
                                            </asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="btnEditUser"
                                runat="server" OnClick="btnEditUser_Click"
                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp;Edit
                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <h2><small>App User Info</small></h2>
                                        </div>
                                    </div>
                                    <hr class="blue-backcolor" />
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12 bg-info bold">
                                            Functional Records : 
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-2 col-md-2 col-lg-2">
                                            <span for="tbUserId" class="required-after">User ID</span>
                                            <asp:TextBox ID="tbUserId" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-sm-2 col-md-2 col-lg-2">
                                            <span for="tbPassword" class="required-after">Password</span>
                                            <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                            <asp:Panel ID="panMsg" runat="server" BackColor="#000099" ForeColor="WhiteSmoke" Width="100%" style="z-index:1000;">
                                               &nbsp;Password must contain at least 
                                                <br />&nbsp;1 Special Character
                                                <br />&nbsp;1 Alpha Character
                                                <br />&nbsp;1 Numeric Character
                                                <br />&nbsp;Do not use space
                                            </asp:Panel>
                                            <ajaxToolkit:PopupControlExtender ID="PopEx"  runat="server" 
                                                TargetControlID="tbPassword"
                                                PopupControlID="panMsg"
                                                Position="Bottom" />
                                        </div>
                                        <div class="form-group col-sm-2 col-md-2 col-lg-2">
                                            <span for="ddlUserGroup">Group</span>
                                            <asp:DropDownList ID="ddlUserGroup" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-md-2 col-lg-2">
                                            <span for="ddlUserTitle">Is Administrator?</span>
                                            <asp:DropDownList ID="ddlIsAdmin" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-md-2 col-lg-2">
                                            <span for="ddlPWD_RESET_REQ">PWD RESET REQ</span>
                                            <asp:DropDownList ID="ddlPWD_RESET_REQ" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-md-2 col-lg-2">
                                            <span for="ddlUserLocation">Location</span>
                                            <asp:DropDownList ID="ddlUserLocation" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12 bg-info bold">
                                            Personal Records : 
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-sm-2 col-md-2 col-lg-2">
                                                    <span for="ddlUserTitle">Title</span>
                                                    <asp:DropDownList ID="ddlUserTitle" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-md-2 col-lg-2">
                                                    <span for="tbUserName" class="required-after">Name</span>
                                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-md-2 col-lg-2">
                                                    <span for="ddlUserDesignation">Designation</span>
                                                    <asp:DropDownList ID="ddlUserDesignation" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-md-2 col-lg-2">
                                                    <span for="ddlUserDepartment">Department</span>
                                                    <asp:DropDownList ID="ddlUserDepartment" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-md-2 col-lg-2">
                                                    <span for="tbUserEmailID" class="required-after">e-Mail ID</span>
                                                    <asp:TextBox ID="tbUserEmailID" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-md-2 col-lg-2">
                                                    <span for="ddlUserCountry">Country</span>
                                                    <asp:DropDownList ID="ddlUserCountry" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12 bg-warning">
                                            This option to create normal application user ?with groups. Who will be able to log in to system and can access modules as authorized. 
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <hr class="blue-backcolor" />
                                    <div class="row">
                                        <div class="col-sm-6 red weight-bold">
                                            All the red (*) marked fields are mandatory
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:LinkButton ID="btnSaveUser"
                                                runat="server" OnClick="btnSaveUser_Click"
                                                CssClass="btn btn-success">
                                <span aria-hidden="true" class="glyphicon glyphicon-save-file"></span>&nbsp;Save
                                            </asp:LinkButton>
                                            &nbsp;
                            <asp:LinkButton ID="btnDelUser"
                                runat="server" OnClick="btnDelUser_Click"
                                CssClass="btn btn-warning">
                                <span aria-hidden="true" class="glyphicon glyphicon glyphicon-trash"></span>&nbsp;Delete
                            </asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="btnCancelUser"
                                                runat="server" OnClick="btnCancelUser_Click"
                                                CssClass="btn btn-info">
                                <span aria-hidden="true" class="glyphicon glyphicon-alert"></span>&nbsp;Clear All
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <%--App User Info End--%>
                            <%--User Access Copy--%>
                            <asp:View ID="vw04" runat="server" OnActivate="vwActivate_EnableLogOff">
                                <div class="container-fluid" style="margin-top: 10px;">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <asp:LinkButton ID="linbtnRefreshCopyAccess"
                                                runat="server" OnClick="linbtnRefreshCopyAccess_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>&nbsp;Refresh
                                            </asp:LinkButton>
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <h2><small>Access Copy Utility</small></h2>
                                        </div>
                                    </div>
                                    <hr class="blue-backcolor" />
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <span for="ddlSourceId">Source User ID</span>
                                            <asp:DropDownList ID="ddlSourceId" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-sm-9 col-lg-9 col-md-9">
                                            <br />
                                            <p class="text-secondary">&lt;&lt; Please select the source user ID ?for whom you have set the module access already from the above list.</p>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <span for="ddlDestUserId" class="required">Target User ID</span>
                                            <asp:DropDownList ID="ddlDestUserId" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-sm-9 col-lg-9 col-md-9">
                                            <br />
                                            <p class="text-secondary">&lt;&lt; Select the Target user ?who’s ID has just created from [App user info] tab.</p>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <hr class="blue-backcolor" />
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6 col-md-6 red weight-bold"></div>
                                        <div class="col-sm-6 col-lg-6 col-md-6" style="text-align: right; left: 0px; top: 0px;">
                                            <asp:LinkButton ID="btnCopyAcc"
                                                runat="server" OnClick="btnCopyAcc_Click"
                                                CssClass="btn btn-success">
                                <span aria-hidden="true" class="glyphicon glyphicon-floppy-saved"></span>&nbsp;Save
                                            </asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="linbtnCancelCopyAcc"
                                                runat="server" OnClick="linbtnCancelCopyAcc_Click"
                                                CssClass="btn btn-info">
                                <span aria-hidden="true" class="glyphicon glyphicon-alert"></span>&nbsp;Clear All
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-sm-12 col-lg-12 col-md-12 red weight-bold">
                                            <br />
                                            <asp:TextBox ID="tbLog" runat="server" CssClass="form-control form-group-sm" Width="100%" Height="65px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <%--User Access Copy End--%>
                            <%--System Admin Info--%>
                            <asp:View ID="vw05" runat="server" OnActivate="vwActivate_EnableLogOff">
                                <div class="container-fluid" style="margin-top: 10px;">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <img src="Picture/usersScrn.png" width="65px" height="65px" alt="" />
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <h2><small>System Admin Info</small></h2>
                                        </div>
                                    </div>
                                    <hr class="blue-backcolor" />
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <asp:ListBox ID="lbUserLists" runat="server" CssClass="form-control form-group-sm" Width="290px" Height="140px"></asp:ListBox>
                                        </div>
                                        <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                            <br />
                                            <asp:LinkButton ID="linbtnLoad"
                                                runat="server" OnClick="btnLoad_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>&nbsp;Load selected user (to edit / delete)
                                            </asp:LinkButton>
                                        </div>
                                        <div class="form-group col-sm-6 col-md-6 col-lg-6">
                                            <div class="row">
                                                <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                                    <span for="tbSysUserId" class="required">User ID</span>
                                                    <asp:TextBox ID="tbSysUserId" runat="server" CssClass="form-control form-group-sm" OnTextChanged="tbSysUserId_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-sm-3 col-md-3 col-lg-3">
                                                    <span for="tbSysUserId" class="required">Password</span>
                                                    <asp:TextBox ID="tbSysPasswd" runat="server" CssClass="form-control form-group-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12">
                                            <p class="text-primary">This options to create ‘system user? who can create / modify and control access to normal user for the system. And this module will only accessible for those user. </p>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <hr class="blue-backcolor" />
                                    <div class="row">
                                        <div class="col-sm-6 red weight-bold">
                                            All the red (*) marked fields are mandatory
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:LinkButton ID="linbtnSysSave"
                                                runat="server" OnClick="linbtnSysSave_Click"
                                                CssClass="btn btn-success">
                                <span aria-hidden="true" class="glyphicon glyphicon-floppy-saved"></span>&nbsp;Save
                                            </asp:LinkButton>
                                            &nbsp;
                            <asp:LinkButton ID="linbtnSysDel"
                                runat="server" OnClick="linbtnSysDel_Click"
                                CssClass="btn btn-warning">
                                <span aria-hidden="true" class="glyphicon glyphicon glyphicon-trash"></span>&nbsp;Delete
                            </asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="linbtnSysCancel"
                                                runat="server" OnClick="linbtnSysCancel_Click"
                                                CssClass="btn btn-info">
                                <span aria-hidden="true" class="glyphicon glyphicon-alert"></span>&nbsp;Clear All
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <%--System Admin Info End--%>
                            <%--Site Access--%>
                            <asp:View ID="vw06" runat="server" OnActivate="vwActivate_EnableLogOff">
                                <div class="container-fluid" style="margin-top: 10px;">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <asp:LinkButton ID="btnSiteCheck"
                                                runat="server" OnClick="btnSiteCheck_Click"
                                                CssClass="btn btn-primary">
                                <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>&nbsp;Check and Update
                                            </asp:LinkButton>&nbsp;                            
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <span for="ddlUserID">User ID</span>
                                            <asp:DropDownList ID="ddlSiteUsers" OnSelectedIndexChanged="ddlSiteUsers_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control form-group-sm"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3" style="text-align: left;"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <h2><small>Site Access</small></h2>
                                        </div>
                                    </div>
                                    <hr class="blue-backcolor" />
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <asp:CheckBox ID="cbSelectSites" runat="server" Text="Select / De-Select Modules" OnCheckedChanged="cbSelectSites_CheckedChanged" AutoPostBack="true" class="form-check-input" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
                                            <asp:Panel ID="panSite" runat="server" Width="100%" Height="300px" BorderStyle="None"
                                                ScrollBars="Auto">
                                                <asp:GridView ID="gvSite" runat="server" Width="100%" Font-Size="8pt" AutoGenerateColumns="False"
                                                    Font-Names="Tahoma" CellPadding="4" OnRowDataBound="RowDataBound_Status" BorderWidth="0px" BorderStyle="None"
                                                    DataKeyNames="PRIKEY" ForeColor="#333333" GridLines="Horizontal">
                                                    <RowStyle BackColor="#FFFFFF" />
                                                    <Columns>
                                                        <asp:BoundField AccessibleHeaderText="RATE" DataField="RATE" HeaderText="RATE" ItemStyle-Width="5px"
                                                            Visible="false" />
                                                        <asp:BoundField AccessibleHeaderText="SITE_GROUP" DataField="SITE_GROUP" HeaderText="SITE GROUP"
                                                            SortExpression="SITE_GROUP" ItemStyle-Width="100px" />
                                                        <asp:BoundField AccessibleHeaderText="SITEID" DataField="SITEID" HeaderText="SITE ID"
                                                            SortExpression="SITEID" ItemStyle-Width="750px" />
                                                        <asp:TemplateField HeaderText="Access" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbAccStat" runat="server" Width="40px" BorderStyle="None" Checked='<%# Bind("[ACCESS]") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#070151B" ForeColor="White" Font-Bold="True" />
                                                    <PagerStyle BackColor="#070151B" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#FFFFCB" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#004300" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <EditRowStyle BackColor="#FFFFFF" ForeColor="Black" />
                                                    <AlternatingRowStyle BackColor="#E3FFDB" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="form-group col-sm-12 col-lg-12 col-md-12 bg-warning">
                                            <p class="text-info">This is System access administration option. you have to create the user ID at the previous step (tab) and then using this option add the same user ID in system user list and assign the site wise access to operate. Beware about the spelling of user id when you are creating it here. </p>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <hr class="blue-backcolor" />
                                    <div class="row">
                                        <div class="col-sm-6 red weight-bold">
                                        </div>
                                        <div class="col-sm-6" style="text-align: right">
                                            <asp:LinkButton ID="btnSaveSite"
                                                runat="server" OnClick="btnSaveSite_Click"
                                                CssClass="btn btn-success">
                                <span aria-hidden="true" class="glyphicon glyphicon-floppy-saved"></span>&nbsp;Save
                                            </asp:LinkButton>
                                            &nbsp;
                            <asp:LinkButton ID="btnDelSite"
                                runat="server" OnClick="btnDelSite_Click"
                                CssClass="btn btn-warning">
                                <span aria-hidden="true" class="glyphicon glyphicon glyphicon-trash"></span>&nbsp;Delete
                            </asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="btnSiteCancel"
                                                runat="server" OnClick="btnSiteCancel_Click"
                                                CssClass="btn btn-info">
                                <span aria-hidden="true" class="glyphicon glyphicon-alert"></span>&nbsp;Clear All
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <%--Site Access End--%>
                        </asp:MultiView>
                        <br />
                        &nbsp;
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <asp:Panel ID="panError" Width="100%" runat="server" CssClass="text-danger" Visible="false">
                            <asp:Label ID="TxtMsgBox" runat="server" BackColor="WhiteSmoke" Width="100%" Font-Bold="true"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
