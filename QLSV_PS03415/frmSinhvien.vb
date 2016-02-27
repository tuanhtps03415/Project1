Public Class frmSinhvien
    'Khai bao bien truy xuat DB tu lop DBAccess
    Private _dbAccess As New DataBaseAccess

    'Khai bao bien de biet trang thai dnag la Edit hay Insert
    Private _isedit As Boolean = False
    Public Sub New(isedit As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _isedit = isedit
    End Sub

    'Dinh nghia ham them ban ghi Sinh vien vao database
    Private Function insertStudent() As Boolean
        Dim sqlQuery As String = "insert into Students (StudentID, StudentName, Phone , Address , ClassID )"
        sqlQuery += String.Format("Values ('{0}','{1}','{2}','{3}','{4}')", _
                                  txtStudentID.Text, txtStudentName.Text, txtPhone.Text, txtAddress.Text, txtClassId.Text)
        Return _dbAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    'Dinh nghia ham Update 
    Private Function updateStudent() As Boolean
        Dim sqlQuery As String = String.Format("UPDATE Students SET StudentName = '{0}', Phone = '{1}', Address = '{2}' where StudentID ='{3}'", _
                                               Me.txtStudentName.Text, Me.txtPhone.Text, Me.txtAddress.Text, Me.txtStudentID.Text)
        Return _dbAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    'Dinh nghia ham kiem tra gia tri truoc khi insert du lieu vao database
    Private Function isEmpty() As Boolean
        Return (String.IsNullOrEmpty(txtStudentID.Text) OrElse String.IsNullOrEmpty(txtStudentName.Text) OrElse _
                 String.IsNullOrEmpty(txtPhone.Text) OrElse String.IsNullOrEmpty(txtAddress.Text) OrElse _
                 String.IsNullOrEmpty(txtClassId.Text))
    End Function

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If isEmpty() Then 'Kiem tra truong du lieu truoc khi thuc hien Them,Sua
            MessageBox.Show("Nhap gia tri vao truoc khi ghi vao database", "error", MessageBoxButtons.OK)
        Else
            If _isedit Then 'Neu la Edit thi goi ham Update
                If updateStudent() Then 'Neu Update thang cong thi thong bao
                    MessageBox.Show("Sua du lieu thang cong!", "Thong bao", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else  'Neu co loi khi sua thi thong bao loi
                    MessageBox.Show("Loi them du lieu", "Error", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            Else            'Neu khong phai edit thi goi ham Insert
                If insertStudent() Then 'Neu insert thanh cong thi thong bao
                    MessageBox.Show("Them du lieu thanh cong!", "Thong bao", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    MessageBox.Show("Loi them du lieu", "Error", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            End If

            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub



End Class