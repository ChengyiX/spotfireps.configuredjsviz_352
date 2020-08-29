using System;
using System.Collections.Generic;
using System.ComponentModel;
using Spotfire.Dxp.Data;
using Spotfire.Dxp.Framework.DocumentModel;
using SpotfirePS.Framework.JSVisualization.Common;
using SpotfirePS.Framework.JSVisualization.Core;
using SpotfirePS.Framework.JSVisualizationForms.Common;

namespace SpotfirePS.ConfiguredJSVizForms
{
    internal sealed partial class DoughnutPropertyPage : PropertyPageBase
    {
        private readonly JSVisualizationModel _model;


        public DoughnutPropertyPage()
        {
            InitializeComponent();
        }

        public DoughnutPropertyPage(JSVisualizationModel model)
            : this()
        {
            _model = model;
        }

        private void ModelChanged()
        {
            ModelChangedCore();
        }

        protected override void ModelChangedCore()
        {

            UpdatedataTableComboBox();
            UpdateColorByComboBox();
            UpdateSizeByComboBox();

            UpdateLabelByComboBox();


        }

        private void UpdateLabelByComboBox()
        {
            try
            {
                labelByComboBox.BeginUpdate();

                labelByComboBox.Items.Clear();
                labelByComboBox.DisplayMember = "Name";
                if (dataTableComboBox.SelectedItem != null)
                {
                    if (_model.DataSettings.Count > 0)
                    {
                        var ds = _model.DataSettings[0];
                        if (ds.View != null)
                        {
                            foreach (var column in ds.View.BaseTableReference.Columns)
                            {
                                
                                if (!column.DataType.IsNumeric)
                                    labelByComboBox.Items.Add(column);
                                if (ds.View.ColumnExpressions.Count > 1 && ds.View.ColumnExpressions[1].Contains(column.NameEscapedForExpression))
                                {
                                    labelByComboBox.SelectedItem = column;
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                labelByComboBox.EndUpdate();
            }

        }



        private void UpdatedataTableComboBox()
        {
            dataTableComboBox.BeginUpdate();

            dataTableComboBox.Items.Clear();
            dataTableComboBox.DisplayMember = "Name";

            foreach (var dataTable in _model.Context.GetService<DataManager>().Tables)
            {
                dataTableComboBox.Items.Add(dataTable);
            }

            if (_model.DataSettings.Count > 0)
            {
                dataTableComboBox.SelectedItem = _model.DataSettings[0].DataTable;
            }

            dataTableComboBox.EndUpdate();
        }


        private void UpdateColorByComboBox()
        {
            segmentByComboBox.BeginUpdate();

            segmentByComboBox.Items.Clear();
            segmentByComboBox.DisplayMember = "Name";
            if (dataTableComboBox.SelectedItem != null)
            {

                foreach (var column in ((DataTable) dataTableComboBox.SelectedItem).Columns)
                {
                    segmentByComboBox.Items.Add(column);
                }

                if (_model.DataSettings.Count > 0)
                {
                    var ds = _model.DataSettings[0];
                    if (ds.View != null)
                    {
                        var viewCols = ds.View.GroupByColumns;
                        if (viewCols.Count > 0)
                        {
                            segmentByComboBox.SelectedItem = viewCols[0];
                        }
                    }
                }
            }

            segmentByComboBox.EndUpdate();
        }

        private void UpdateSizeByComboBox()
        {
            try
            {



                sizeByComboBox.BeginUpdate();

                sizeByComboBox.Items.Clear();
                sizeByComboBox.DisplayMember = "Name";
                if (dataTableComboBox.SelectedItem != null)
                {

                    if (_model.DataSettings.Count > 0)
                    {
                        var ds = _model.DataSettings[0];
                        if (ds.View != null)
                        {
                            foreach (var column in ds.View.BaseTableReference.Columns)
                            {
                                if (column.DataType.IsNumeric)
                                {
                                    sizeByComboBox.Items.Add(column);
                                }

                                if (ds.View.ColumnExpressions.Count > 0 && ds.View.ColumnExpressions[0]
                                        .Contains(column.NameEscapedForExpression))
                                {
                                    sizeByComboBox.SelectedItem = column;
                                }
                            }

                            if (ds.View.ColumnExpressions.Count > 0)
                            {
                                if (ds.View.ColumnExpressions[0].StartsWith("Rank"))
                                {
                                    comboBoxFunction.SelectedIndex = 2;
                                }
                                else if
                                    (ds.View.ColumnExpressions[0].StartsWith("Avg"))
                                {
                                    comboBoxFunction.SelectedIndex = 1;
                                }
                                else
                                    comboBoxFunction.SelectedIndex = 0;


                            }
                        }


                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                sizeByComboBox.EndUpdate();
            }
         
        }

        protected override Trigger GetUpdateTriggersCore()
        {
            if (_model.DataSettings[0] == null)
            {
                _model.DataSettings[0] = new DataSetting("Default");
                _model.DataSettings[0].AutoConfigure();
            }

            var ds = _model.DataSettings[0];
            return
                Trigger.CreateCompositeTrigger(

                    //DataTables and column changes
                    Trigger.CreatePropertyTrigger(_model.Context.GetService<DataManager>().Tables,
                        DataTableCollection.PropertyNames.Items),
                    Trigger.CreatePropertyTrigger(ds, DataSetting.PropertyNames.DataTable),
                    Trigger.CreateMutablePropertyTrigger(ds, DataSetting.PropertyNames.DataTable,
                        (DataTable table) =>
                            Trigger.CreatePropertyTrigger(table.Columns,
                                DataColumnCollection.PropertyNames.Items)),
                    Trigger.CreateMutablePropertyTrigger(ds, DataSetting.PropertyNames.DataTable,
                        (DataTable table) =>
                            Trigger.CreateSubTreeTrigger(table.Columns, typeof(DataColumn),
                                DataColumn.PropertyNames.Name)),
                    //Markings
                    Trigger.CreatePropertyTrigger(ds.Context.GetService<DataManager>().Markings,
                        DataMarkingSelectionCollection.PropertyNames.Items),
                    Trigger.CreatePropertyTrigger(ds, DataSetting.PropertyNames.Marking),
                    Trigger.CreatePropertyTrigger(ds.LimitByMarkings,
                        MarkingReferenceList.PropertyNames.Items),
                    Trigger.CreatePropertyTrigger(ds, DataSetting.PropertyNames.ShowAllOnEmptyMarking),
                    //Filterings
                    Trigger.CreatePropertyTrigger(ds.Context.GetService<DataManager>().Filterings,
                        DataFilteringSelectionCollection.PropertyNames.Items),
                    Trigger.CreatePropertyTrigger(ds, DataSetting.PropertyNames.Filtering),
                    Trigger.CreatePropertyTrigger(ds,
                        DataSetting.PropertyNames.PageRowData,
                        DataSetting.PropertyNames.PageRowSize,
                        DataSetting.PropertyNames.UsePlotManagedPaging),
                    //Trigger.CreatePropertyTrigger(ds, DataSetting.PropertyNames.FilterByExpression),
                    Trigger.CreateMutablePropertyTrigger(ds, DataSetting.PropertyNames.View,
                        (PersistentDataView pdv) =>
                            Trigger.CreatePropertyTrigger(pdv,
                                PersistentDataView.PropertyNames.GroupByColumns,
                                PersistentDataView.PropertyNames.ColumnExpressions,
                                PersistentDataView.PropertyNames.SortBy)));
        }


        private void sizeByComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (IsUpdating) return;
            var ds = _model.DataSettings[0];

            var expressions = new List<string>(ds.View.ColumnExpressions);
            var column = sizeByComboBox.SelectedItem as DataColumn;
            if (column == null) return;

            var expressionFormat = comboBoxFunction.SelectedItem as string;
            if (expressionFormat == null) return;

            expressions[0] = string.Format(expressionFormat, column.NameEscapedForExpression);
            ds.View.ColumnExpressions = expressions;

        }


        private void dataTableComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (IsUpdating) return;
            _model.DataSettings[0].DataTable = dataTableComboBox.SelectedItem as DataTable;
        }

        private void colorByComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (IsUpdating) return;
            _model.DataSettings[0].View.GroupByColumns =
                new List<DataColumn>(new[] {segmentByComboBox.SelectedItem as DataColumn});
        }


        private void buttonApplySize_Click(object sender, System.EventArgs e)
        {

        }


        private void labelByComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (IsUpdating) return;

            var column = labelByComboBox.SelectedItem as DataColumn;
            var expression = $@"First({column?.NameEscapedForExpression})";

            var ds = _model.DataSettings[0];
            var expressions = new List<string>(ds.View.ColumnExpressions);
            
            if (expressions.Count < 2)
                expressions.Add(expression);
            else
            {
                expressions[1] = expression;
            }

            ds.View.ColumnExpressions = expressions;
        }
    }
}
