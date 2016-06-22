Imports FluentAssertions
Imports MediatR
Imports Microsoft.Practices.Unity
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class FabriqueInjectionDependancesTest

    <TestMethod()>
    Public sub ObtenirInstance_Devrait_Configurer_Mediator()
        dim sujet = new FabriqueInjectionDependances()

        Dim Injecteur as IUnityContainer = sujet.ObtenirInstance()

        Injecteur.Should().NotBeNull()

        Injecteur.Resolve(Of IMediator)().Should().NotBeNull()
    End sub 
End Class

Public Class FabriqueInjectionDependances
    Public Function ObtenirInstance() As IUnityContainer
        Dim injecteur = new UnityContainer()
        injecteur.RegisterType(Of IMediator, Mediator)
        injecteur.RegisterInstance(Of SingleInstanceFactory)(Function(t) injecteur.Resolve(t))
        injecteur.RegisterInstance(Of MultiInstanceFactory)(Function(t) injecteur.ResolveAll(t))
        return injecteur
    End Function
End Class
