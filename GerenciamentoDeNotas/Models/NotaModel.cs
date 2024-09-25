namespace GerenciamentoDeNotas.Models
{
    public class NotaModel
    {
        public int Id { get; set; }
        public AlunoModel Aluno { get; set; }
        public MateriaProfessorModel Materia { get; set; }
        public double Prova1 { get; set; } = 0;
        public double Prova2 { get; set; } = 0;
        public double Prova3 { get; set; } = 0;
        public double Trabalho { get; set; } = 0;
        public double NotaTotal
        {
            get { return SomarNotas(); }
        }

        private double SomarNotas()
        {
            return Prova1 + Prova2 + Prova3 + Trabalho;
        }

    }
}
