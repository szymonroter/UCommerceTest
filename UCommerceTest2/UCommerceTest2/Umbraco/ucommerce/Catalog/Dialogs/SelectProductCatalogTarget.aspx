<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../masterpages/Dialog.master" CodeBehind="SelectProductCatalogTarget.aspx.cs" Inherits="UCommerce.Web.UI.Catalog.Dialogs.SelectProductCatalogTarget" %>

<%@ import namespace="UCommerce.Web.UI.Catalog.Dialogs" %>
<%@ register tagprefix="uc" tagname="CatalogItemSelector" src="CatalogItemSelector.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderLabel" runat="server">
    <asp:Localize ID="Localize5" runat="server" meta:resourceKey="Header" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentArea" runat="server">
    <div class="propertyPane dialog-header">
        <h3>
            <span>
                <%= GetLocalResourceObject("Header.Text") %>
            </span>
        </h3>
        <p class="guiDialogTiny">
            <%= GetLocalResourceObject("SubHeader.Text") %>
        </p>
    </div>
    <div class="propertyPane contentCatalogItems">
	    <div class="propertyItems">
        <uc:CatalogItemSelector runat="server" id="catalogItemSelector" SelectType="<%# CatalogItemType.ProductCatalog %>"
            MultipleSelect="false" />
		</div>
	</div>
    <div class="propertyPane dialog-actions">
	    <div class="footerOkCancel">
			<asp:Button id="SaveButton" CssClass="dialog-saveButton" runat="server" onclick="SaveButton_Clicked" />
			<em><%= GetLocalResourceObject("Or.Text") %> </em><a href="#" class="dialog-cancelButton" onclick="UCommerceClientMgr.closeModalWindow()">
				<%= GetLocalResourceObject("CancelButton.Text") %>
			</a>	
		</div>
    </div>
</asp:Content>
