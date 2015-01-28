using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    public class ProcessControllerFactory
    {
        private static ProcessController m_processController;

        public static ProcessController GetProcessController()
        {
            if (m_processController == null)
            {
                m_processController = new ProcessController();
            }
            return m_processController;
        }
    }
}
