<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Pivotal.Master" AutoEventWireup="true" CodeBehind="Cookies.aspx.cs" Inherits="FortunesFormsUI.Cookies" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="Title">Fortune Cookies</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <div class="row">
            <div class="large-12 columns">
                <div class="panel">
                    <h3>
                        <asp:Label runat="server" ID="lblCookie"></asp:Label>
                    </h3>
                    <hr/>
                    <b>Provider: </b><asp:Label runat="server" ID="lblCookieProvider"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="large-12 columns">
                    <asp:Button runat="server" ID="btnGetCookie" Text="Show me my Fortune" OnClick="btnGetCookie_OnClick" CssClass="medium button"/>
                    <asp:Button ID="btnKill" runat="server" Text="Kill" OnClick="btnKill_OnClick" CssClass="medium alert button"/>
                </div>
            </div>
        </div>
    </form>
</asp:Content>