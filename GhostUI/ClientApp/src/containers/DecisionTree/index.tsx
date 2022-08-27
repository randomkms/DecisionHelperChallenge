import { useEffect, useRef, useState, type FunctionComponent } from 'react';
import Tree from 'react-d3-tree';
import { useAppDispatch, useAppSelector } from 'src/store';
import { getDecisionTreeAsync, type DecisionNode } from 'src/store/decisionTreeSlice';
import type { Point, RawNodeDatum } from 'react-d3-tree/lib/types/common';
import { useParams } from 'react-router-dom';

const renderNodeWithCustomEvents = ({ nodeDatum }: { nodeDatum: RawNodeDatum }, visitedNodesIds: Set<string>) => {
    const isLeaf = !nodeDatum.children?.length
    const maxNameLength = 24;
    const formattedName = nodeDatum.name.length > maxNameLength ? nodeDatum.name.substring(0, maxNameLength) + "..." : nodeDatum.name;
    const rectWidth = 190;
    const reactHeight = 50;
    const rectX = -rectWidth / 2;
    const calculatedTextX = nodeDatum.name.length > maxNameLength ? rectX : -nodeDatum.name.length * 4;
    const answer = nodeDatum.attributes!.answer as string; // TODO add calculated width
    const isVisitedNode = visitedNodesIds.has(nodeDatum.attributes!.id as string);

    return <g>
        <rect fill={isVisitedNode ? "yellow" : ""} width={rectWidth} height={reactHeight} x={rectX} y={-reactHeight / 2} opacity={0.5} strokeWidth={1}>
            <title>{nodeDatum.name}</title>
        </rect>
        {answer &&
            <text fill="black" strokeWidth="1" stroke={"grey"} x={-answer.length * 4} dy={-60}>
                {answer}
                <title>{answer}</title>
            </text>
        }

        <text stroke={isLeaf ? "red" : "black"} x={calculatedTextX} y={5} strokeWidth={1}>
            <title>{nodeDatum.name}</title>
            {formattedName}
        </text>
    </g >
};

const mapDecisionTree = (treeRoot: DecisionNode) => {
    const result: RawNodeDatum = {
        name: treeRoot.result ?? treeRoot.question,
        attributes: {
            id: treeRoot.id,
            answer: treeRoot.answer
        }
    }

    result.children = treeRoot.children.length ? treeRoot.children.map(c => mapDecisionTree(c)) : undefined;

    return result;
}

const DecisionTree: FunctionComponent = () => {
    const dispatch = useAppDispatch();
    const decisionTree = useAppSelector<DecisionNode | null>((state) => state.decisionTree.treeRoot);
    const visitedNodesIds = useAppSelector<string[]>((state) => state.decisionTree.visitedNodesIds);
    const { treeName } = useParams();
    const treeContainerRef = useRef<HTMLDivElement>(null);
    const [startingTranslate, setStartingTranslate] = useState<Point>();

    useEffect(() => {
        if (treeName)
            dispatch(getDecisionTreeAsync(treeName));
    }, [dispatch, treeName]);

    useEffect(() => {
        if (!treeContainerRef?.current)
            return;

        const dimensions = treeContainerRef.current.getBoundingClientRect();
        setStartingTranslate({
            x: dimensions.width / 2,
            y: dimensions.height / 20,
        });
    }, []);

    if (!decisionTree)
        return null;

    const treeData = mapDecisionTree(decisionTree);

    return (
        <div className="section">
            <div ref={treeContainerRef} id="treeWrapper" style={{ width: 'auto', height: '54em' }}>
                <Tree data={treeData} orientation={'vertical'} collapsible={false}
                    nodeSize={{ x: 200, y: 150 }} translate={startingTranslate}
                    renderCustomNodeElement={(props) => renderNodeWithCustomEvents(props, new Set(visitedNodesIds))} />
            </div>
        </div>
    );
}

export default DecisionTree;