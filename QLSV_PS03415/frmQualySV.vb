Public Class FRM_QLSV
    'Khai bao bien de truy xuat DB tu lop DatabaseAccess
    Private _DBAccess As New DataBaseAccess

    'Khai bao bien trang thai kiem tra du lieu dang load 
    Private _isLoading As Boolean = False

    'Dinh nghia thu tuc load du lieu tu bang Lop vao  Combobox
    Private Sub LoadDataVaoCombobox()
        Dim sqlQuery As String = "select ClassID, ClassName from Class"
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.cmbClass.DataSource = dTable
        Me.cmbClass.ValueMember = "ClassID"
        Me.cmbClass.DisplayMember = "ClassName"
    End Sub

    'Dinh nghia thu tuc load du lieu tu bang Sinh vien theo tung lop vao Gridview
    Private Sub LoadDataVaoGridView(ClassID As String)
        Dim sqlQuery As String = _
            String.Format("Select StudentId, StudentName, Phone, Address from Students where ClassId = '{0}'", ClassID)
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvStudents.DataSource = dTable
        With Me.dgvStudents
            .Columns(0).HeaderText = "Student ID"
            .Columns(1).HeaderText = "Student Name"
            .Columns(2).HeaderText = "Phone"
            .Columns(3).HeaderText = "Address"
        End With
    End Sub

    Private Sub FRM_QLSV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _isLoading = True  'True khi du lieu bat dau load

        LoadDataVaoCombobox()
        LoadDataVaoGridView(Me.cmbClass.SelectedValue)

        _isLoading = False  'False khi du lieu xong
    End Sub

    Private Sub cmbClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClass.SelectedIndexChanged
        If Not _isLoading Then 'Neu load du lieu xong'
            LoadDataVaoGridView(Me.cmbClass.SelectedValue)
        End If
    End Sub

    'Dinh nghia thu tuc hien thi ket qua search
    'Private Sub SearchStudent(ClassID As String, value As String)
    'Dim sqlQuery As String = _
    'String.Format("Select StudentId, StudentName, Phone, Address from Students where ClassId = '{0}'", ClassID)
    'If Me.cmbSearch.SelectedIndex = 0 Then  'Tim theo Student ID
    'sqlQuery += String.Format(" AND StudentID LIKE '{0}%'", value)
    'ElseIf Me.cmbSearch.SelectedIndex = 1 Then 'Tim theo Student Name
    ' sqlQuery += String.Format(" AND StudentName LIKE '{0}%'", value)
    '  End If
    'Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
    'Me.dgvStudents.DataSource = dTable
    '  With Me.dgvStudents
    ' .Columns(0).HeaderText = "Student ID"
    '  .Columns(1).HeaderText = "Student Name"
    '.Columns(2).HeaderText = "Phone"
    ' .Columns(3).HeaderText = "Address"
    ' End With
    ' End Sub


    'Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
    ' SearchStudent(Me.cmbClass.SelectedValue, Me.txtSearch.Text)
    ' End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frm As New frmSinhvien(False)
        frm.txtClassId.Text = Me.cmbClass.SelectedValue
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            'Load du lieu
            LoadDataVaoGridView(Me.cmbClass.SelectedValue)
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim frm As New frmSinhvien(True)
        frm.txtClassId.Text = Me.cmbClass.SelectedValue
        With Me.dgvStudents
            frm.txtStudentID.Text = .Rows(.CurrentCell.RowIndex).Cells("StudentId").Value
            frm.txtStudentName.Text = .Rows(.CurrentCell.RowIndex).Cells("StudentName").Value
            frm.txtPhone.Text = .Rows(.CurrentCell.RowIndex).Cells("Phone").Value
            frm.txtAddress.Text = .Rows(.CurrentCell.RowIndex).Cells("Address").Value
        End With
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then 'Sua du lieu thanh cong thi load lai du lieu vao gridview
            LoadDataVaoGridView(Me.cmbClass.SelectedValue)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'Khai bao bien lay Student ID ma dong can xoa da duoc chon tren Gridview
        Dim studentID As String = Me.dgvStudents.Rows(Me.dgvStudents.CurrentCell.RowIndex).Cells("StudentID").Value

        'Khai bao cau lenh Query de xoa
        Dim sqlQuery As String = String.Format("Delete students Where studentID = '{0}'", studentID)

        'Thuc hien xoa
        If _DBAccess.ExecuteNoneQuery(sqlQuery) Then 'Xoa thanh cong thi thong bao
            MessageBox.Show("Da xoa du lieu thanh cong!")
            'Load lai du lieu  tren Gridview
            LoadDataVaoGridView(Me.cmbClass.SelectedValue)
        Else
            MessageBox.Show("Loi xoa du lieu!")
        End If
    End Sub
End Class
