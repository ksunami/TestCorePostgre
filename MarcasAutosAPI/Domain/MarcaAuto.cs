namespace MarcasAutosAPI.Domain
{
    /// <summary>
    /// Representa una marca de auto con campos de auditoría.
    /// </summary>
    public class MarcaAuto
    {
        /// <summary>
        /// Identificador único de la marca de auto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la marca del auto.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Fecha y hora en que el registro fue creado.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identificador del usuario que creó el registro.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Fecha y hora en que el registro fue modificado por última vez.
        /// </summary>
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Identificador del usuario que modificó el registro por última vez.
        /// </summary>
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// Indica si el registro ha sido eliminado lógicamente.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Timestamp para la concurrencia optimista.
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}
