Imports System.Xml

Public Class clsU90Help

  Public hlpXML As XmlDocument
  Private currentPage As XmlElement
  Private mModeEnabled As Boolean
  Public currentName As String
  Public currentNumber As Long
  Public totalNumber As Long
  Public currentXml As String

  Public Sub New(ByRef refHelp As XmlDocument)

    hlpXML = refHelp
  End Sub

  Public Sub DisplayStepInfo(ByVal intStep As Long, ByVal tStep As TextBox, ByVal tName As TextBox, ByVal tHelp As RichTextBox)

    Dim theElement, thePage As XmlElement
    Dim i As Integer

    tStep.Text = intStep.ToString

    currentNumber = intStep
    theElement = hlpXML.DocumentElement

    totalNumber = theElement.ChildNodes.Count
    For i = 0 To theElement.ChildNodes.Count - 1
      thePage = theElement.ChildNodes(i)
      currentPage = thePage

      If intStep.ToString() = thePage.ChildNodes(0).InnerText Then
        tName.Text = thePage.ChildNodes(1).InnerText
        currentName = tName.Text
        tHelp.Rtf = thePage.ChildNodes(2).InnerText
        currentXml = tHelp.Rtf
      End If

    Next

    hasChanged = False
  End Sub

  Public Function AddNewPage(ByVal pageName As String) As Boolean

    Dim thePage, theNumber, theName, theText As XmlElement

    thePage = hlpXML.CreateElement("HelpPage")
    totalNumber += 1

    theNumber = hlpXML.CreateElement("Number")
    theNumber.InnerText = totalNumber.ToString
    thePage.AppendChild(theNumber)

    theName = hlpXML.CreateElement("Name")
    theName.InnerText = "New Page"
    thePage.AppendChild(theName)

    theText = hlpXML.CreateElement("Rtf")
    theText.InnerText = ""
    thePage.AppendChild(theText)

    hlpXML.DocumentElement.AppendChild(thePage)

    totalNumber = hlpXML.DocumentElement.ChildNodes.Count
  End Function

  Public Function SavePageName(ByVal intStep As Long, ByVal pageName As String) As Boolean

    If Not currentPage Is Nothing Then
      currentPage.ChildNodes(1).InnerText = pageName
      Return True
    Else
      Return False
    End If
  End Function

  Public Function SavePage(ByVal intStep As Long, ByVal rtfText As String) As Boolean

    If Not currentPage Is Nothing Then
      currentPage.ChildNodes(2).InnerText = rtfText
      Return True
    Else
      Return False
    End If
  End Function

End Class
