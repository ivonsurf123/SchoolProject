// ==================== FECHA ACTUAL (Reutilizando Función) ====================
document.addEventListener('DOMContentLoaded', function () {
    const dateElement = document.getElementById('currentDate');
    if (dateElement) {
        const today = new Date();
        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        dateElement.textContent = today.toLocaleDateString('es-ES', options);
    }
});

// ==================== ABRIR MODAL DE EDICIÓN ====================
document.addEventListener('DOMContentLoaded', function () {
    const editButtons = document.querySelectorAll('.edit-btn');

    editButtons.forEach(button => {
        button.addEventListener('click', function () {
            // Obtener datos de la calificación
            const scoreId = this.getAttribute('data-id');
            const studentId = this.getAttribute('data-studentid');
            const studentName = this.getAttribute('data-studentname');
            const courseId = this.getAttribute('data-courseid');
            const courseName = this.getAttribute('data-coursename');
            const evaluationType = this.getAttribute('data-evaluationtype');
            const value = this.getAttribute('data-value');
            const date = this.getAttribute('data-date'); // Formato yyyy-MM-dd para input type="date"
            const period = this.getAttribute('data-period');
            const observations = this.getAttribute('data-observations');

            // Rellenar campos del modal de edición
            document.getElementById('EditScoreId').value = scoreId;

            // Campos de solo lectura (display)
            document.getElementById('EditStudentName').value = studentName;
            document.getElementById('EditCourseName').value = courseName;

            // Campos ocultos (para enviar en el POST, ya que no son editables)
            document.getElementById('EditStudentIdHidden').value = studentId;
            document.getElementById('EditCourseIdHidden').value = courseId;

            // Campos editables
            document.getElementById('EditEvaluationType').value = evaluationType;
            document.getElementById('EditValue').value = value;
            document.getElementById('EditDate').value = date;
            document.getElementById('EditPeriod').value = period;
            document.getElementById('EditObservations').value = observations || ''; // Asegurar que sea string vacío si es null

            // Abrir modal
            const editModal = new bootstrap.Modal(document.getElementById('editScoreModal'));
            editModal.show();
        });
    });
});

// ==================== ABRIR MODAL DE ELIMINACIÓN ====================
document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-btn');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function () {
            const scoreId = this.getAttribute('data-id');
            const studentName = this.getAttribute('data-student');
            const courseName = this.getAttribute('data-course');
            const evaluationType = this.getAttribute('data-type');

            // Rellenar datos en el modal
            document.getElementById('deleteScoreId').value = scoreId;
            document.getElementById('scoreInfoToDelete').textContent =
                `Estudiante: ${studentName}, Curso: ${courseName}, Evaluación: ${evaluationType}`;

            // Abrir modal
            const deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
            deleteModal.show();
        });
    });
});

// ==================== LIMPIAR FORMULARIO AL CERRAR MODAL ====================
document.addEventListener('DOMContentLoaded', function () {
    const createModal = document.getElementById('scoreModal');

    if (createModal) {
        createModal.addEventListener('hidden.bs.modal', function () {
            // Limpiar los campos del formulario de creación al cerrarse
            document.getElementById('scoreForm').reset();
        });
    }
});