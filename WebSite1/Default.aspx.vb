
Partial Class _Default
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Label1.Text = System.Configuration.ConfigurationManager.ConnectionStrings("My_ConnectionString").ConnectionString
    End Sub
End Class
