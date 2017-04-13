<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cookies.aspx.cs" Inherits="FortunesFormsUI.Cookies" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
     .small {
         font-size: x-small
     }
</style>
    <div style="position: absolute;top: 50%;left: 50%;height: 30%;width: 50%;margin: -15% 0 0 -25%;">
    <asp:Panel runat="server" ID="pnlCookieResult" BorderStyle="Double" Visible="False">
        <asp:Label runat="server" ID="lblCookie"></asp:Label>
    </asp:Panel>
    <br/><br/>
    <asp:Button runat="server" ID="btnGetCookie" Text="Show me my Fortune" OnClick="btnGetCookie_OnClick"/>
        </div>
    
    
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Label runat="server" ID="lblCookieProvider" CssClass="small"></asp:Label>
</asp:Content>
