<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Async="true" Inherits="GESTAO_DE_CLIENTES.Pages.Clientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/JavaScript/TabPage.js"></script>
    <%--seção de cadastro--%>

    <div id='divCadastro' style="margin: 1.5rem;" class="collapse show">
        <div id="tabPage" class="m-3">
            <div id="tabPageHeader">
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <button class="nav-link active" aria-current="page" data-type="header-button" data-target="content1" data-default-page="true">Cadastro de Cliente</button>
                    </li>
                </ul>
            </div>
            <div id="tabPageContent" style="" class="">
                <asp:Panel runat="server" class="collapse collapse-vertical show" ID="pnlCadastro" ClientIDMode="Static" data-type="page">
                    <div style="margin-top: 10px; display: flex; justify-content: flex-start">
                        <div>
                            <label for="txtNomeCliente">Nome do Cliente</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtNomeCliente" errorMessageID="lblValidacaoNomeCliente" class="form-control" placeholder="Ex.: João Silva" ValidationGroup="CadastroValidation"></asp:TextBox>
                            <asp:Label runat="server" ID="lblValidacaoNomeCliente" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                        </div>
                        <div style="margin-left: 20px">
                            <label for="txtSexo">Sexo</label>
                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="txtSexo" errorMessageID="lblValidacaoSexo" class="form-control" placeholder="Sexo" ValidationGroup="CadastroValidation">
                                <asp:ListItem>M</asp:ListItem>
                                <asp:ListItem>F</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblValidacaoSexo" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                        </div>
                        <div style="margin-left: 20px">
                            <label for="txtDataNascimento">Data de Nascimento</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDataNascimento" errorMessageID="lblValidacaoDataNascimento" class="form-control" Width="120px" type="date" ValidationGroup="CadastroValidation"></asp:TextBox>
                            <asp:Label runat="server" ID="lblValidacaoDataNascimento" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                        </div>
                    </div>
                    <div style="display: flex; justify-content: flex-start">
                        <div>
                            <label for="txtCPF">CPF</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCPF" errorMessageID="lblValidacaoCPF" class="form-control" placeholder="123.456.789-10" ValidationGroup="CadastroValidation"></asp:TextBox>
                            <asp:Label runat="server" ID="lblValidacaoCPF" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                            <ajaxToolkit:MaskedEditExtender ID="maskCPF" runat="server" TargetControlID="txtCPF" Mask="999,999,999-99" MaskType="Number" />
                        </div>
                        <div style="margin-left: 20px; text-align: left; width: 120px">
                            <label for="txtSituacao">Situação</label>
                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="txtSituacao" DataValueField="cD_Situacao" DataTextField="dC_Situacao" errorMessageID="lblValidacaoSituacao" class="form-control" placeholder="Ex.: Ativo" ValidationGroup="CadastroValidation" OnPreRender="txtSituacao_PreRender"></asp:DropDownList>
                            <asp:Label runat="server" ID="lblValidacaoSituacao" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                        </div>
                    </div>
                    <div style="margin-top: 10px; display: flex; justify-content: flex-start">
                        <asp:Button runat="server" ClientIDMode="Static" ID="btnCadastrarCliente" Text="Cadastrar" CssClass="btn btn-dark" Width="100px" ValidationGroup="CadastroValidation" OnClick="btnCadastrarCliente_Click" OnClientClick="validaDataNascimento();" />
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
        <asp:GridView ID="gridCliente" ClientIDMode="Static" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" DataKeyNames="id,iD_Situacao" runat="server" class="table table-hover table-bordered table-responsive table-border-factor" OnPreRender="gridCliente_PreRender" OnPageIndexChanging="gridCliente_PageIndexChanging">
            <HeaderStyle CssClass="table-dark " />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Ação
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/images/icon-edit.png" AlternateText="Editar" CssClass="icon" OnClick="btnEditar_Click" />
                        <asp:ImageButton ID="btnExcluir" ClientIDMode="Static" CausesValidation="false" runat="server" ImageUrl="~/images/icon-bin.png" isValid="true" AlternateText="Excluir" CssClass="icon" OnClick="btnExcluir_Click" />
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:BoundField DataField="id"            HeaderText="id" Visible="false" />
                <asp:BoundField DataField="nome"          HeaderText="Cliente" />
                <asp:BoundField DataField="cpf"           HeaderText="CPF"                HeaderStyle-Width="180px" DataFormatString="{0:###\.###\.###-##}"   />
                <asp:BoundField DataField="nascimento"    HeaderText="Data de Nascimento" HeaderStyle-Width="180px" DataFormatString="{0:dd/MM/yyyy}"    />
                <asp:BoundField DataField="sexo"          HeaderText="Sexo"               HeaderStyle-Width="60px"  />
                <asp:BoundField DataField="iD_Situacao"   HeaderText="id_Situação"        HeaderStyle-Width="180px" Visible="false" />
                <asp:BoundField DataField="situacao"      HeaderText="Situação"           HeaderStyle-Width="180px"  />
                <asp:BoundField DataField="alteracao"     HeaderText="Última Alteração"   HeaderStyle-Width="160px" />
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
                    <asp:Panel runat="server" ID="pnlModalEdicao" ClientIDMode="Static">
                        <div style="margin-top: 10px; display: flex; justify-content: flex-start">
                            <div>
                                <label for="txtModalNomeCliente">Nome do Cliente</label>
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtModalNomeCliente" errorMessageID="lblValidacaoModalNomeCliente" class="form-control" placeholder="Ex.: João Silva" ValidationGroup="ModalUpdateValidation"></asp:TextBox>
                                <asp:Label runat="server" ID="lblValidacaoModalNomeCliente" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                            </div>
                            <div style="margin-left: 20px">
                                <label for="txtModalSexo">Sexo</label>
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="txtModalSexo" errorMessageID="lblValidacaoModalSexo" class="form-control" placeholder="Sexo" ValidationGroup="ModalUpdateValidation">
                                    <asp:ListItem>M</asp:ListItem>
                                    <asp:ListItem>F</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label runat="server" ID="lblValidacaoModalSexo" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                            </div>
                            <div style="margin-left: 20px">
                                <label for="txtModalDataNascimento">Data de Nascimento</label>
                                <asp:TextBox runat="server" onblur="" ClientIDMode="Static" ID="txtModalDataNascimento" errorMessageID="lblValidacaoModalDataNascimento" class="form-control" Width="120px" TextMode="Date" ValidationGroup="ModalUpdateValidation"></asp:TextBox>
                                <asp:Label runat="server" ID="lblValidacaoModalDataNascimento" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                            </div>
                        </div>
                        <div style="display: flex; justify-content: flex-start">
                            <div>
                                <label for="txtModalCPF">CPF</label>
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtModalCPF" errorMessageID="lblValidacaoModalCPF" class="form-control" placeholder="123.456.789-10" ValidationGroup="ModalUpdateValidation"></asp:TextBox>
                                <asp:Label runat="server" ID="lblValidacaoModalCPF" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtModalCPF" Mask="999,999,999-99" MaskType="Number" />
                            </div>
                            <div style="margin-left: 20px; text-align: left; width: 120px">
                                <label for="txtModalSituacao">Situação</label>
                                <asp:DropDownList runat="server" ClientIDMode="Static" DataValueField="iD_Situacao" DataTextField="situacao" ID="txtModalSituacao" errorMessageID="lblValidacaoModalSituacao" class="form-control" placeholder="Ex.: Ativo" ValidationGroup="ModalUpdateValidation" ></asp:DropDownList>
                                <asp:Label runat="server" ID="lblValidacaoModalSituacao" Visible="false" ForeColor="Red" Text="Campo Obrigatório"></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>
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
