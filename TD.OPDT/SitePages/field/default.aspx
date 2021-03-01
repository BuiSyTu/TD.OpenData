<%@ Page Language="C#" MasterPageFile="~sitecollection/_catalogs/masterpage/default/default.master" inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document"%>
<%@ Register Tagprefix="layout" Namespace="TD.Core.Layouts.Controls" Assembly="TD.Core.Layouts.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fdcb66d7090aabcd" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageHeaderTitle">
	Lĩnh vực
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
	<!--begin:: Widgets/Input methods-->
	<div class="m-portlet mb-0">
		<div class="m-portlet__head">
			<div class="m-portlet__head-caption">
				<div class="m-portlet__head-title">
					<h3 class="m-portlet__head-text">
						Danh sách các lĩnh vực
					</h3>
				</div>
			</div>
			<div class="m-portlet__head-tools">

				<div class="btn-group">
					<button type="button" btn-adddata class="btn btn-outline-success m-btn m-btn--icon m-btn--pill">
						<span><i class="flaticon-add"></i><span>Thêm mới</span></span>
					</button>
				</div>
			</div>
		</div>
		<!--begin::Form-->
		<div class="m-portlet__body">
			<!--begin: Datatable -->
			<table class="td-datatable table table-bordered m-table display" style="width:100%">
			</table>
			<!--end: Datatable -->
		</div>
		<!--end::Form-->
	</div>
	<!--end:: Widgets/Input methods-->

	<script id="add-template" type="text/html">
        <div class="m-form m-form--label-align-right p-3 row" tdf-type="form">
            <div class="form-group m-form__group row col-12">
                <label class="col-form-label col-3">Tên lĩnh vực</label>
                <div class="col-9">
                    <input name="Name" class="form-control" type="text" placeholder="Tiêu đề">
                </div>
			</div>
			<div class="form-group m-form__group row col-12">
				<label class="col-form-label col-3">Sử dụng</label>
				<div class="col-9">
					<label class="m-checkbox m-checkbox--state-success">
						<input type="checkbox" name="Active" value="true" checked="checked">
						<span></span>
					</label>
				</div>
			</div>
            <div class="form-group m-form__group row col-12">
                <label class="col-form-label col-3">Mã</label>
                <div class="col-9">
                    <input name="Code" class="form-control" type="text" placeholder="Mã">
                </div>
			</div>
			<div class="form-group m-form__group row col-12">
                <label class="col-form-label col-3">Thứ tự</label>
                <div class="col-9">
                    <input name="Order" class="form-control" type="number">
                </div>
			</div>
        </div>
    </script>

	<script id="edit-template" type="text/html">
        <div class="m-form m-form--label-align-right p-3 row" tdf-type="form">
            <div class="form-group m-form__group row col-12">
                <label class="col-form-label col-3">Tên lĩnh vực</label>
                <div class="col-9">
                    <input name="Name" class="form-control" type="text" placeholder="Tiêu đề">
                </div>
			</div>
			<div class="form-group m-form__group row col-12">
				<label class="col-form-label col-3">Sử dụng</label>
				<div class="col-9">
					<label class="m-checkbox m-checkbox--state-success">
						<input type="checkbox" id="Active" name="Active" value="true" checked="checked">
						<span></span>
					</label>
				</div>
			</div>
            <div class="form-group m-form__group row col-12">
                <label class="col-form-label col-3">Mã</label>
                <div class="col-9">
                    <input name="Code" class="form-control" type="text" placeholder="Mã">
                </div>
			</div>
			<div class="form-group m-form__group row col-12">
                <label class="col-form-label col-3">Thứ tự</label>
                <div class="col-9">
                    <input name="Order" class="form-control" type="number">
                </div>
			</div>
        </div>
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageVendorStyles">
	<link type="text/css" rel="stylesheet" href="~/_layouts/15/tdcore/v3/assets/vendors/custom/datatables/datatables.bundle.css"/>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageVendorScripts">
	<script type="text/javascript" src="~/_layouts/15/tdcore/v3/assets/vendors/custom/datatables/datatables.bundle.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageScripts">
	<script type="text/javascript" src="default.js"> </script>
</asp:Content>
