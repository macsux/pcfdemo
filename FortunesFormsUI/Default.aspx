<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FortunesFormsUI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: absolute;top: 50%;left: 50%;height: 30%;width: 50%;margin: -15% 0 0 -25%;">
    <asp:Panel runat="server" ID="pnlCookieResult" BorderStyle="Double" Visible="False">
        <asp:Label runat="server" ID="lblCookie"></asp:Label>
    </asp:Panel>
    <br/><br/>
    <asp:Button runat="server" ID="btnGetCookie" Text="Show me my Fortune" OnClick="btnGetCookie_OnClick"/>
        </div>
</asp:Content>
