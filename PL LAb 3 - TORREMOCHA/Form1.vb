Public Class Form1
    Dim productions As New Dictionary(Of String, List(Of List(Of String))) From {
    {"exp", New List(Of List(Of String)) From {New List(Of String) From {"term", "exp'"}}},
    {"exp'", New List(Of List(Of String)) From {
        New List(Of String) From {"addop", "term", "exp'"},
        New List(Of String) From {"λ"}
    }},
    {"addop", New List(Of List(Of String)) From {
        New List(Of String) From {"+"},
        New List(Of String) From {"-"}
    }},
    {"term", New List(Of List(Of String)) From {New List(Of String) From {"factor", "term'"}}},
    {"term'", New List(Of List(Of String)) From {
        New List(Of String) From {"mulop", "factor", "term'"},
        New List(Of String) From {"λ"}
    }},
    {"mulop", New List(Of List(Of String)) From {
        New List(Of String) From {"*"}
    }},
    {"factor", New List(Of List(Of String)) From {
        New List(Of String) From {"(", "exp", ")"},
        New List(Of String) From {"num"}
    }}
}

    Dim FIRST As New Dictionary(Of String, HashSet(Of String))
    Dim FOLLOW As New Dictionary(Of String, HashSet(Of String))


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtbOutput.Clear()
        rtbOutput.AppendText("Grammar Production Rules:" & vbCrLf)

        For Each key As String In productions.Keys
            Dim productionList As New List(Of String)

            ' Flatten the inner lists into a single list of production rules
            For Each innerList As List(Of String) In productions(key)
                productionList.Add(String.Join(" ", innerList)) ' Join the inner list into a string
            Next

            ' Join the outer list of production rules with " | "
            Dim rule As String = key & " → " & String.Join(" | ", productionList)
            rtbOutput.AppendText(rule & vbCrLf)
        Next
    End Sub


    ' Change all dgvSteps to dgvFirstFollow
    Sub ComputeFIRST()
        Dim changed As Boolean
        Do
            changed = False
            For Each lhs In productions.Keys
                If Not FIRST.ContainsKey(lhs) Then FIRST(lhs) = New HashSet(Of String)

                For Each rule In productions(lhs)
                    Dim nullable = True
                    For Each symbol In rule
                        If Not FIRST.ContainsKey(symbol) Then FIRST(symbol) = New HashSet(Of String)
                        If Not productions.ContainsKey(symbol) Then
                            ' Terminal
                            If FIRST(lhs).Add(symbol) Then changed = True
                            nullable = False
                            Exit For
                        Else
                            Dim before = FIRST(lhs).Count
                            FIRST(lhs).UnionWith(FIRST(symbol).Where(Function(s) s <> "λ"))
                            If FIRST(lhs).Count > before Then changed = True
                            If Not FIRST(symbol).Contains("λ") Then
                                nullable = False
                                Exit For
                            End If
                        End If
                    Next
                    If nullable Then
                        If FIRST(lhs).Add("λ") Then changed = True
                    End If
                Next
            Next
        Loop While changed
    End Sub

    Sub ComputeFOLLOW()
        For Each nt In productions.Keys
            FOLLOW(nt) = New HashSet(Of String)
        Next
        FOLLOW("exp").Add("$")

        Dim changed As Boolean
        Do
            changed = False
            For Each lhs In productions.Keys
                For Each rule In productions(lhs)
                    For i = 0 To rule.Count - 1
                        Dim B = rule(i)
                        If Not productions.ContainsKey(B) Then Continue For
                        Dim followSet As New HashSet(Of String)
                        Dim nullable = True

                        For j = i + 1 To rule.Count - 1
                            Dim sym = rule(j)
                            If Not FIRST.ContainsKey(sym) Then FIRST(sym) = New HashSet(Of String)
                            followSet.UnionWith(FIRST(sym).Where(Function(s) s <> "λ"))
                            If Not FIRST(sym).Contains("λ") Then
                                nullable = False
                                Exit For
                            End If
                        Next

                        If nullable Then followSet.UnionWith(FOLLOW(lhs))
                        Dim before = FOLLOW(B).Count
                        FOLLOW(B).UnionWith(followSet)
                        If FOLLOW(B).Count > before Then changed = True
                    Next
                Next
            Next
        Loop While changed
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dgvFirstFollow.Columns.Clear()
        dgvFirstFollow.Rows.Clear()

        dgvFirstFollow.Columns.Add("NonTerminal", "Non-Terminal")
        dgvFirstFollow.Columns.Add("First", "First")
        dgvFirstFollow.Columns.Add("Follow", "Follow")

        FIRST.Clear()
        FOLLOW.Clear()
        ComputeFIRST()
        ComputeFOLLOW()

        For Each nt In productions.Keys
            Dim firstSet = String.Join(" ", FIRST(nt).OrderBy(Function(x) x))
            Dim followSet = String.Join(" ", FOLLOW(nt).OrderBy(Function(x) x))
            dgvFirstFollow.Rows.Add(nt, firstSet, followSet)
        Next

        dgvFirstFollow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub



End Class
