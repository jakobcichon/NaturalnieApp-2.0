using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaturalnieApp2.Controls.NaturalnieMessageBox
{
    /// <summary>
    /// Interaction logic for UserControlWithMessageBox.xaml
    /// </summary>
    public partial class UserControlWithMessageBox : UserControl
    {

        private Point _clickPoint;
        private FrameworkElement _dragObject;
        private Canvas _referenceCanvasObject;

        private ReferencePoints _anchorPoint;

        public ReferencePoints AnchorPoint
        {
            get { return _anchorPoint; }
            set { _anchorPoint = value; }
        }

        public enum ReferencePoints
        {
            TOP_LEFT,
            TOP_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_RIGHT,
        }

        public UserControlWithMessageBox()
        {
            InitializeComponent();

            // Initialize Framework Elements
            _dragObject = MessageBox;
            _referenceCanvasObject = MessageBoxCanvas;
            AnchorPoint = ReferencePoints.TOP_RIGHT;

        }

        private static Point GetMidPositionForControl(Canvas parentControl, FrameworkElement objectToPosition)
        {
            // Get canvas dimensions
            double canvasWidth = parentControl.ActualWidth;
            double canvasHeight = parentControl.ActualHeight;

            // Get object dimensions
            double objectWidth = objectToPosition.ActualHeight;
            double objectHeight = objectToPosition.ActualHeight;  

            // Calculate center point
            double x = (canvasWidth - objectWidth) / 2;
            double y = (canvasHeight - objectHeight) / 2;

            return new Point(x, y);
        }

        private void MessageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MoveObject(e.GetPosition(_dragObject));

                e.Handled = true;
            }
        }

        private void MessageBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MoveObject(e.GetPosition(_dragObject));
            }
        }

        private void MoveObject(Point mousePosition)
        {
            Point pointDelta = CalculatePointDelta(_clickPoint, mousePosition, AnchorPoint);

            Point dragObjectReferencePoint = GetDragObjectReferenceToCanvas(_dragObject, AnchorPoint);

            Point newPoint = CalculateDragElementNewReferencePoint(dragObjectReferencePoint, pointDelta);

            MoveElement(_dragObject, newPoint, AnchorPoint);

        }

        private static void MoveElement(FrameworkElement dragElement, Point newReferencePoint, ReferencePoints referencePoints=ReferencePoints.TOP_LEFT)
        {
            switch (referencePoints)
            {
                case ReferencePoints.TOP_LEFT:
                    Canvas.SetLeft(dragElement, newReferencePoint.X);
                    Canvas.SetTop(dragElement, newReferencePoint.Y);
                    break;
                case ReferencePoints.TOP_RIGHT:
                    Canvas.SetRight(dragElement, newReferencePoint.X);
                    Canvas.SetTop(dragElement, newReferencePoint.Y);
                    break;
                case ReferencePoints.BOTTOM_RIGHT:
                    Canvas.SetRight(dragElement, newReferencePoint.X);
                    Canvas.SetBottom(dragElement, newReferencePoint.Y);
                    break;
                case ReferencePoints.BOTTOM_LEFT:
                    Canvas.SetLeft(dragElement, newReferencePoint.X);
                    Canvas.SetBottom(dragElement, newReferencePoint.Y);
                    break;
            }
        }

        private static Point CalculateDragElementNewReferencePoint(Point referencePoint, Point moveDelta)
        {
            Point returnPoint;

            returnPoint.X = referencePoint.X + moveDelta.X;
            returnPoint.Y = referencePoint.Y + moveDelta.Y;

            return returnPoint;
        }

        private static Point GetDragObjectReferenceToCanvas(FrameworkElement dragObject, ReferencePoints referencePointType=ReferencePoints.TOP_LEFT)
        {
            Point returnPoint;

            switch(referencePointType)
            {
                case ReferencePoints.TOP_LEFT:
                    returnPoint.X = Canvas.GetLeft(dragObject);
                    returnPoint.Y = Canvas.GetTop(dragObject);
                    break;
                case ReferencePoints.TOP_RIGHT:
                    returnPoint.X = Canvas.GetRight(dragObject);
                    returnPoint.Y = Canvas.GetTop(dragObject);
                    break;
                case ReferencePoints.BOTTOM_RIGHT:
                    returnPoint.X = Canvas.GetRight(dragObject);
                    returnPoint.Y = Canvas.GetBottom(dragObject);
                    break;
                case ReferencePoints.BOTTOM_LEFT:
                    returnPoint.X = Canvas.GetLeft(dragObject);
                    returnPoint.Y = Canvas.GetBottom(dragObject);
                    break;
            }

            return returnPoint;
        }

        private static Point CalculatePointDelta(Point referencePoint, Point movedPoint, ReferencePoints referencePointType=ReferencePoints.TOP_LEFT)
        {
            Point returnPoint;

            switch (referencePointType)
            {
                case ReferencePoints.TOP_LEFT:
                    returnPoint.X = movedPoint.X - referencePoint.X;
                    returnPoint.Y = movedPoint.Y - referencePoint.Y;
                    break;
                case ReferencePoints.TOP_RIGHT:
                    returnPoint.X = referencePoint.X - movedPoint.X;
                    returnPoint.Y = movedPoint.Y - referencePoint.Y;
                    break;
                case ReferencePoints.BOTTOM_RIGHT:
                    returnPoint.X = referencePoint.X - movedPoint.X;
                    returnPoint.Y = referencePoint.Y - movedPoint.Y;
                    break;
                case ReferencePoints.BOTTOM_LEFT:
                    returnPoint.X = movedPoint.X - referencePoint.X;
                    returnPoint.Y = referencePoint.Y - movedPoint.Y;
                    break;
            }

            return returnPoint;
        }

        #region Events
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Size delta = e.NewSize.Width;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Calculate mid position
            Point midPoint = GetMidPositionForControl(_referenceCanvasObject, _dragObject);

            // Set drag object possition
            MoveElement(_dragObject, midPoint, AnchorPoint);
        }

        private void MessageBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _clickPoint = e.GetPosition(sender as FrameworkElement);



        }
        #endregion

    }
}
