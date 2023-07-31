using GESTAO_DE_CLIENTES.API;
using GESTAO_DE_CLIENTES.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace GESTAO_DE_CLIENTES.Pages
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected SiteMaster master { get { return (SiteMaster)this.Master; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(CarregaGridViewAsync));
                RegisterAsyncTask(new PageAsyncTask(CarregaCampoDropDownListAsync));
            }
        }


        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnEditar = sender as ImageButton;

            try
            {
                GridView gridCliente = (GridView)btnEditar.NamingContainer.NamingContainer;

                int index = ((GridViewRow)btnEditar.NamingContainer).DataItemIndex;
                int colunaIndex = GetColumnIndex(gridCliente, "nome");
                string key = gridCliente.DataKeys[index].Values["id"].ToString();

                CarregaCampoDropDownListModal();

                TableCellCollection tb = gridCliente.Rows[index].Cells;

                txtModalCPF.Text = tb[GetColumnIndex(gridCliente, "cpf")].Text;
                txtModalDataNascimento.Text = Convert.ToDateTime(tb[GetColumnIndex(gridCliente, "nascimento")].Text).ToString("yyyy-MM-dd");
                txtModalNomeCliente.Text = tb[GetColumnIndex(gridCliente, "nome")].Text;
                txtModalSexo.Text = tb[GetColumnIndex(gridCliente, "sexo")].Text;
                txtModalSituacao.SelectedValue = gridCliente.DataKeys[index].Values["iD_Situacao"].ToString();

                hiddenKey.Value = key.ToString();

                ScriptManager.RegisterStartupScript(this, GetType(), "myModal", "$('#myModal').modal('show');", true);
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        protected async void btnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnExcluir = sender as ImageButton;
            APIService api = new APIService();
            try
            {
                GridView gridCliente = (GridView)btnExcluir.NamingContainer.NamingContainer;

                int index = ((GridViewRow)btnExcluir.NamingContainer).DataItemIndex;
                string key = gridCliente.DataKeys[index].Value.ToString();

                await api.Delete("cliente", int.Parse(key));

                await CarregarGridCliente();

                master.ShowMessageOnTop("Cliente excluído com sucesso!", "success");
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }

        }
        protected async void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            APIService api = new APIService();
            if (ValidaCampoVazio(pnlCadastro, "CadastroValidation"))
            {
                try
                {
                    if (!master.ValidaCpf((txtCPF.Text).Replace(".", "").Replace("-", "")))
                    {
                        throw new Exception("CPF inválido");
                    }
                    Cliente cliente = new Cliente
                    {
                        ID = 0,
                        Nome = txtNomeCliente.Text,
                        Nascimento = DateTime.Parse(txtDataNascimento.Text),
                        CPF = long.Parse((txtCPF.Text).Replace(".", "").Replace("-", "")),
                        Sexo = txtSexo.Text,
                        Situacao = txtSituacao.Text,
                        ID_Situacao = int.Parse(txtSituacao.SelectedValue)
                    };

                    await api.Post("cliente", api.ConvertToJson(cliente));

                    await CarregarGridCliente();
                    limpaCampos();

                    master.ShowMessageOnTop("Cliente cadastrado com sucesso!", "success");
                }
                catch (Exception ex)
                {
                    master.AlertModal("Ocorreu um erro!", ex.Message);
                }

            }

        }
        protected void gridCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                try
                {
                    await CarregarGridCliente();

                    gridCliente.PageIndex = e.NewPageIndex;
                    gridCliente.DataBind();
                }
                catch (Exception ex)
                {
                    master.AlertModal("Ocorreu um erro!", ex.Message);
                }
            }));
        }
        protected async void btnSalvarAlteracao_Click(object sender, EventArgs e)
        {
            if (ValidaCampoVazio(pnlModalEdicao, "ModalUpdateValidation"))
            {
                APIService api = new APIService();
                try
                {
                    if (!master.ValidaCpf((txtModalCPF.Text).Replace(".", "").Replace("-", "")))
                    {
                        throw new Exception("CPF inválido");
                    }

                    Cliente cliente = new Cliente
                    {
                        ID = int.Parse(hiddenKey.Value),
                        Nome = txtModalNomeCliente.Text,
                        Nascimento = DateTime.Parse(txtModalDataNascimento.Text),
                        CPF = long.Parse((txtModalCPF.Text).Replace(".", "").Replace("-", "")),
                        Sexo = txtModalSexo.Text,
                        Situacao = txtModalSituacao.Text,
                        ID_Situacao = int.Parse(txtModalSituacao.SelectedValue)
                    };

                    await api.Put("cliente", api.ConvertToJson(cliente));

                    await CarregarGridCliente();

                    master.ShowMessageOnTop("Informações alteradas com sucesso!", "success");
                }
                catch (Exception ex)
                {
                    master.AlertModal("Ocorreu um erro!", ex.Message);
                }

            }
        }
        protected async void gridCliente_PreRender(object sender, EventArgs e)
        {
            try
            {
                await CarregarGridCliente();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        protected async void txtSituacao_PreRender(object sender, EventArgs e)
        {
            try
            {
                await CarregaCampoDropDownList(txtSituacao);
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        private bool ValidaCampoVazio(Panel painelCampos, string validationGroup)
        {
            bool isValid = true;
            try
            {
                foreach (Control control in painelCampos.Controls)
                {

                    if (control.GetType().Name == "TextBox")
                    {
                        if (((TextBox)control).ValidationGroup == validationGroup)
                        {

                            TextBox txtCampo = control as TextBox;
                            Label lblErrorMessage = painelCampos.FindControl(txtCampo.Attributes["errorMessageID"]) as Label;

                            if (string.IsNullOrEmpty(txtCampo.Text.Trim()))
                            {
                                lblErrorMessage.Visible = true;
                                isValid = false;
                            }
                            else
                            {
                                lblErrorMessage.Visible = false;
                            }
                        }
                    }
                    else if (control.GetType().Name == "DropDownList")
                    {
                        if (((DropDownList)control).ValidationGroup == validationGroup)
                        {

                            DropDownList ddlCampo = control as DropDownList;
                            Label lblErrorMessage = painelCampos.FindControl(ddlCampo.Attributes["errorMessageID"]) as Label;

                            if (string.IsNullOrEmpty(ddlCampo.SelectedValue))
                            {
                                lblErrorMessage.Visible = true;
                                isValid = false;
                            }
                            else
                            {
                                lblErrorMessage.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
            return isValid;
        }
        private async Task CarregaCampoDropDownList(DropDownList campoSituacao)
        {
            try
            {
                DataTable dt = await new APIService().Get("situacao");
                campoSituacao.DataSource = dt;
                campoSituacao.DataBind();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        private void CarregaCampoDropDownListModal()
        {
            try
            {
                txtModalSituacao.Items.Clear();
                foreach (ListItem item in txtSituacao.Items)
                {
                    txtModalSituacao.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        protected async Task CarregaGridViewAsync()
        {
            try
            {
                await CarregarGridCliente();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        protected async Task CarregaCampoDropDownListAsync()
        {
            try
            {
                await CarregaCampoDropDownList(txtSituacao);

            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        private int GetColumnIndex(GridView grid, string columnName)
        {
            int index = -1;
            foreach (DataControlField coluna in grid.Columns)
            {
                if (coluna is BoundField boundField && boundField.DataField == columnName)
                {
                    index = grid.Columns.IndexOf(coluna);
                    break;
                }
            }
            return index;
        }
        private async Task CarregarGridCliente()
        {
            GridView gridCliente = (GridView)pnlDados.FindControl("gridCliente");

            try
            {
                DataTable dt = await new APIService().Get("cliente");
                gridCliente.DataSource = dt;
                gridCliente.DataBind();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        protected void limpaCampos()
        {
            txtCPF.Text = string.Empty;
            txtDataNascimento.Text = string.Empty;
            txtNomeCliente.Text = string.Empty;
        }
    }
}
