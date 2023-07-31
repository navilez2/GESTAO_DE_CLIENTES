<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Situacao.aspx.cs" Async="true" Inherits="GESTAO_DE_CLIENTES.Pages.Situacao" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/JavaScript/TabPage.js"></script>
    <%--seção de cadastro--%>

    <div id='divCadastro' style="margin: 1.5rem;" class="collapse show">
        <div id="tabPage" class="m-3">
            <div id="tabPageHeader">
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <button class="nav-link active" aria-current="page" data-type="header-button"
                            data-target="content1" data-default-page="true">
                            Cadastro de Situação</button>
                    </li>
                </ul>
            </div>
            <div id="tabPageContent" style="" class="">
                <asp:Panel runat="server"  class="collapse collapse-vertical show" id="pnlCadastro" ClientIDMode="Static" data-type="page">
                    <div style="margin-top: 10px; display: flex; justify-content: flex-start">
                        <div>
                            <label for="txtSituacao">Situação</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSituacao" class="form-control" placeholder="Ex.: Ativo" ValidationGroup="CadastroValidation"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="CadastroValidation" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" runat="server" ControlToValidate="txtSituacao" ErrorMessage="Campo obrigatório." />
                        </div>
                    </div>
                    <div style="margin-top: 10px; display: flex; justify-content: flex-start">
                        <asp:Button runat="server" ClientIDMode="Static" ID="btnCadastrarCliente"  Text="Cadastrar" CssClass="btn btn-dark" Width="100px" ValidationGroup="CadastroValidation" OnClick="btnCadastrarCliente_Click" OnClientClick="validaDataNascimento();" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <button id="btoDivCadastro" class="btn btn-dark" type="button" data-bs-toggle="collapse"
        data-bs-target="#divCadastro" aria-expanded="true" aria-controls="divCadastro"
        style="width: 100%; border-radius: 0%; height: 29px; padding: 0; background-color: transparent; border-bottom: 1px solid #DEE2E6; border-top: none; border-left: none; border-right: none; color: black;">
        ▲
    </button>

    <%--seção de visualização--%>
    <asp:Panel runat="server" ID="pnlDados" Style="margin: 1.5rem">
        <asp:GridView ID="gridSituacao" ClientIDMode="Static" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" DataKeyNames="cD_SITUACAO" runat="server" class="table table-hover table-bordered table-responsive table-border-factor" OnPreRender="gridSituacao_PreRender" OnPageIndexChanging="gridSituacao_PageIndexChanging">
            <HeaderStyle CssClass="table-dark " />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Ação
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/images/icon-edit.png" AlternateText="Editar" CssClass="icon" OnClick="btnEditar_Click" />
                        <asp:ImageButton ID="btnExcluir" CausesValidation="false" runat="server" ImageUrl="~/images/icon-bin.png" AlternateText="Excluir" CssClass="icon" OnClick="btnExcluir_Click" OnClientClick="function(s,e){if (!confirm('Deseja realmente excluir este item?')) {
                event.preventDefault();
            }}" />
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:BoundField DataField="cD_SITUACAO" HeaderText="id" Visible="false" />
                <asp:BoundField DataField="dC_SITUACAO" SortExpression="situacao" HeaderText="Situação" />
                <asp:BoundField DataField="dT_ALTERACAO" SortExpression="alteracao" HeaderText="Última Alteração" HeaderStyle-Width="160px" />
            </Columns>
            <PagerSettings Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" PageButtonCount="5" />
            <PagerStyle CssClass="pagination justify-content-center custom-pagination" />
        </asp:GridView>
    </asp:Panel>



    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edição</h5>
                    <button type="button" class="btn-close close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hiddenKey" />

                    <div style="margin-top: 10px; display: flex; justify-content: flex-start">
                        <div>
                            <label for="txtModalSituacao">Situação</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtModalSituacao" class="form-control" placeholder="Ex.: João Silva" ValidationGroup="ModalUpdateValidation"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="ModalUpdateValidation" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" runat="server" ControlToValidate="txtModalSituacao" ErrorMessage="Campo obrigatório." />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                    <asp:Button runat="server" ClientIDMode="Static" ID="btnSalvarAlteracao" type="button" class="btn btn-primary" Text="Salvar" ValidationGroup="ModalUpdateValidation" OnClick="btnSalvarAlteracao_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        divCadastro.addEventListener('hidden.bs.collapse', function () {
            btoDivCadastro.textContent = '▼'
        });
        divCadastro.addEventListener('shown.bs.collapse', function () {
            btoDivCadastro.textContent = '▲'
        });

        var btnExcluir = document.getElementById('btnExcluir')
        btnExcluir.addEventListener('click', function (s, e) {
            if (!confirm('Deseja realmente excluir este item?')) {
                event.preventDefault();
            }
        })
    </script>
</asp:Content>
