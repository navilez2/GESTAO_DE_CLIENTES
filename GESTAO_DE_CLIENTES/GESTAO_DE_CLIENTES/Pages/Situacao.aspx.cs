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
    public partial class Situacao : System.Web.UI.Page
    {
        protected SiteMaster master { get { return (SiteMaster)this.Master; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadGridViewAsync));
            }
        }
        protected async Task LoadGridViewAsync()
        {
            try
            {
                await CarregarGridSituacao();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnEditar = sender as ImageButton;

            try
            {
                GridView gridSituacao = (GridView)btnEditar.NamingContainer.NamingContainer;

                int index = ((GridViewRow)btnEditar.NamingContainer).DataItemIndex;
                int colunaIndex = GetColumnIndex(gridSituacao, "dC_SITUACAO");
                string key = gridSituacao.DataKeys[index].Values["cD_SITUACAO"].ToString();

                TableCellCollection tb = gridSituacao.Rows[index].Cells;

                txtModalSituacao.Text = tb[GetColumnIndex(gridSituacao, "dC_SITUACAO")].Text;

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
                GridView gridSituacao = (GridView)btnExcluir.NamingContainer.NamingContainer;

                int index = ((GridViewRow)btnExcluir.NamingContainer).DataItemIndex;
                string key = gridSituacao.DataKeys[index].Value.ToString();

                await api.Delete("situacao", int.Parse(key));

                await CarregarGridSituacao();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        protected async void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            APIService api = new APIService();
            if (Page.IsValid)
            {

                try
                {
                    API.Models.Situacao situacao = new API.Models.Situacao
                    {
                        DC_SITUACAO = txtSituacao.Text
                    };

                    await api.Post("situacao", api.ConvertToJson(situacao));

                    await CarregarGridSituacao();
                    limpaCampos();

                    master.ShowMessageOnTop("Situação cadastrada com sucesso!", "success");
                }
                catch (Exception ex)
                {
                    master.AlertModal("Ocorreu um erro!", ex.Message);
                }

            }

        }
        protected async void btnSalvarAlteracao_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                APIService api = new APIService();
                try
                {
                    API.Models.Situacao situacao = new API.Models.Situacao
                    {
                        CD_SITUACAO = int.Parse(hiddenKey.Value),
                        DC_SITUACAO = txtModalSituacao.Text
                    };

                    await api.Put("situacao", api.ConvertToJson(situacao));

                    await CarregarGridSituacao();
                    limpaCampos();

                    master.ShowMessageOnTop("Informações alteradas com sucesso!", "success");
                }
                catch (Exception ex)
                {
                    master.AlertModal("Ocorreu um erro!", ex.Message);
                }

            }
        }
        protected async void gridSituacao_PreRender(object sender, EventArgs e)
        {
            try
            {
                await CarregarGridSituacao();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }

        protected async void gridSituacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                try
                {
                    await CarregarGridSituacao();

                    gridSituacao.PageIndex = e.NewPageIndex;
                    gridSituacao.DataBind();
                }
                catch (Exception ex)
                {
                    master.AlertModal("Ocorreu um erro!", ex.Message);
                }
            }));
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
        private async Task CarregarGridSituacao()
        {
            GridView gridSituacao = (GridView)pnlDados.FindControl("gridSituacao");

            try
            {
                DataTable dt = await new APIService().Get("situacao");
                gridSituacao.DataSource = dt;
                gridSituacao.DataBind();
            }
            catch (Exception ex)
            {
                master.AlertModal("Ocorreu um erro!", ex.Message);
            }
        }
        protected void limpaCampos()
        {
            txtSituacao.Text = string.Empty;
        }
    }
}
