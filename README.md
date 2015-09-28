# Multiple Environment Support For .NET WebApplications

ASP.NET 5 introduces improved support for controlling application behavior across multiple environments, such as development, staging, and production. See http://docs.asp.net/en/beta5/fundamentals/environments.html for more informations. This assembly aims to give support for such multiple environments for ASP.NET before version 5.


## Development

This should be the environment used when developing an application.

## Staging
Pre-production environment used for final testing before deployment to production. Ideally, its physical characteristics should mirror that of production, so that any issues that may arise in production occur first in the staging environment, where they can be addressed without impact to users.

## Production
The Production environment is the environment in which the application runs when it is live and being used by end users. This environment should be configured to maximize security, performance, and application robustness. Some common settings that a production environment might have that would differ from development include:

 - Turn on caching
 - Ensure all client-side resources are bundled, minified, and potentially served from a CDN
 - Turn off diagnostic ErrorPages
 - Turn on friendly error pages
 - Enable production logging and monitoring (e.g. AppInsights)


## Global.asax

```c#
<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Dim i As env.net.WebConfig = New env.net.WebConfig()
        i.patch()
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

</script>
```