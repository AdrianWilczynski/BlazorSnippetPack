# Blazor Snippet Pack

A snippet pack for Blazor.


## Razor (aspnetcorerazor, razor)

| Prefix             | Renders                                        |
|--------------------|------------------------------------------------|
| functions          | @functions block                               |
| bl-router-fallback | Router with Fallback Component                 |
| bl-route-param     | Route with parameter (@page directive) |
| bl-navlink         | NavLink                                        |
| bl-form-group      | Form Group (label, input, validation message)  |
| bl-form            | Form with validation                           |

### Components

| Prefix                 | Renders                                                        |
|------------------------|----------------------------------------------------------------|
| bl-component           | Component                                                      |
| bl-component-http      | Component with HTTP call                                       |
| bl-component-inherits  | Component inheriting Component Base (code-behind)              |
| bl-component-child     | Component with Child Content                                   |
| bl-component-templated | Templated Component                                            |


## C# (csharp)

| Prefix                | Renders                     |
|-----------------------|-----------------------------|
| foreach-index         | Foreach with index          |
| bl-param              | Parameter                   |
| bl-param-cascading    | Cascading Parameter         |
| bl-eventHandler       | Event Handler               |
| bl-eventHandler-async | Async Event Handler         |
| bl-eventCallback      | Event Callback              |
| bl-js-invoke          | Invoke JavaScript Function  |

### Components

| Prefix            | Renders        |
|-------------------|----------------|
| bl-component-base | Component Base |

### Lifecycle methods

| Prefix                   | Renders              |
|--------------------------|----------------------|
| bl-onInit                | OnInit               |
| bl-onInit-async          | OnInitAsync          |
| bl-onParametersSet       | OnParametersSet      |
| bl-onParametersSet-async | OnParametersSetAsync |
| bl-onAfterRender         | OnAfterRender        |
| bl-onAfterRender-async   | OnAfterRenderAsync   |


## JavaScript (javascript)

| Prefix                      | Renders                     |
|-----------------------------|-----------------------------|
| bl-cs-invoke-static         | Invoke C# Static Method     |


## HTML (html)

### Single Page Apps for GitHub Pages

See: https://github.com/rafrex/spa-github-pages & https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/blazor/client-side?view=aspnetcore-3.0#github-pages

| Prefix                 | Renders                                               |
|------------------------|-------------------------------------------------------|
| github-pages-spa-index | Index Script from Single Page Apps for GitHub Pages   |
| github-pages-spa-404   | 404 Page from Single Page Apps for GitHub Pages       |
| github-pages-base      | Base Path                                             |