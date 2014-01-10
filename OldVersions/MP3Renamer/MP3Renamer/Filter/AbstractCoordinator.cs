using System.Collections.Generic;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Interfaces;


namespace MP3Renamer.Models.Extraction
{
    public abstract class AbstractCoordinator
    {
        protected abstract IEnumerable<IRootWorkunit> RootWorkunit();
        protected abstract IEnumerable<ISubrootWorkunit> SubrootWorkunit();
        protected abstract IEnumerable<ILeafWorkunit> LeafWorkunit();

        public void Execute(IRoot Root)
        {
            // erst die root filter
            Root = ExecuteAllRootFilter(Root);

            // dann die subroot filter 
            for(int i = 0; i< Root.SubRoots.Count; i++) 
            {
                Root.SubRoots[i] = ExecuteAllSubRootFilter(Root.SubRoots[i]);

                // dann die leaf filter 
                for (int j = 0; j < Root.SubRoots[i].Leafs.Count; j++)
                {
                    Root.SubRoots[i].Leafs[j] = ExecuteAllLeafFilter(Root.SubRoots[i].Leafs[j]);
                }
            }
        }


        private IRoot ExecuteAllRootFilter(IRoot root)
        {
            foreach (var rootexcuter in RootWorkunit())
            {
                rootexcuter.Workunit = root;
                ((IExecuteable)rootexcuter).Execute();
                root = rootexcuter.Workunit;
            }
            return root;
        }


        private ISubRoot ExecuteAllSubRootFilter(ISubRoot subroot)
        {
            foreach (var subrootexcuter in SubrootWorkunit())
            {
                subrootexcuter.Workunit = subroot;
                ((IExecuteable)subrootexcuter).Execute();
                subroot = subrootexcuter.Workunit;
            }
            return subroot;
        }

        private ILeaf ExecuteAllLeafFilter(ILeaf leaf)
        {
            foreach (var leafexcuter in LeafWorkunit())
            {
                leafexcuter.Workunit = leaf;
                ((IExecuteable)leafexcuter).Execute();
                leaf = leafexcuter.Workunit;
            }
            return leaf;
        }

        
    }
}